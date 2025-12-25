using NodeMap.Core.Models;
using NodeMap.Core.Algorithms;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Windows.Forms.VisualStyles;

namespace NodeMap.UI
{
    public partial class Form1 : Form
    {
        private Graph? _graph;
        private List<Node> _activeNodes = new();
        private List<Node> _shortestPath = new();
        private string _activeAlgorithm = "";
        private Dictionary<Node, int> _degrees = new();
        private List<Node> _centralNodes = new();
        private Dictionary<Node, int> _centrality = new();
        private List<Node> _topCentralNodes = new();
        private Dictionary<Node, double> _centralityValues = new();

        private float _zoom = 1.0f;
        private bool _drawEdges = true;


        private Rectangle _canvasRect = new Rectangle(10, 10, 900, 600);

        private int _nextNodeId = 1;
        private Random _rnd = new Random();

        private Node? _draggedNode = null;

        private Node? _rightClickedNode;





        private ContextMenuStrip _edgeMenu = new();

        private Node? _selectedNode;   
        private ContextMenuStrip _nodeMenu = new();

        private Node? _contextNode;   
        private Node? _dragNode;      
        private Node? _edgeStartNode; 
        private bool _isDragging;
        private Point _dragOffset;

        private Edge? _selectedEdge = null;

        private long _lastAlgorithmTimeMs = 0;












        public Form1()
        {
            InitializeComponent();
            this.Paint += Form1_Paint;
            this.MouseWheel += Form1_MouseWheel;
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;

            InitNodeContextMenu();
            InitEdgeContextMenu();


        }

        private void InitEdgeContextMenu()
        {
            _edgeMenu.Items.Clear();

            // EDGE WEIGHT DEĞİŞTİR
            _edgeMenu.Items.Add("Weight Değiştir", null, (s, e) =>
            {
                if (_selectedEdge == null) return;

                string input = Microsoft.VisualBasic.Interaction.InputBox(
                    "Yeni edge weight:",
                    "Edge Weight",
                    _selectedEdge.Weight.ToString()
                );

                if (int.TryParse(input, out int newWeight))
                {
                    _selectedEdge.Weight = newWeight;
                    Invalidate();
                }
                else if (!string.IsNullOrWhiteSpace(input))
                {
                    MessageBox.Show("Geçerli bir sayı giriniz");
                }
            });

            // EDGE SİL
            _edgeMenu.Items.Add("Edge Sil", null, (s, e) =>
            {
                if (_selectedEdge == null || _graph == null) return;
                _graph.Edges.Remove(_selectedEdge);
                _selectedEdge = null;
                Invalidate();
            });
        }


        private Edge? GetEdgeAt(Point mouse)
        {
            int tolerance = 6;

            foreach (var edge in _graph!.Edges)
            {
                var p1 = new Point(
                    _canvasRect.X + (int)(edge.Source.X * _zoom) + (int)(30 * _zoom),
                    _canvasRect.Y + (int)(edge.Source.Y * _zoom) + (int)(30 * _zoom)
                );
                var p2 = new Point(
                    _canvasRect.X + (int)(edge.Target.X * _zoom) + (int)(30 * _zoom),
                    _canvasRect.Y + (int)(edge.Target.Y * _zoom) + (int)(30 * _zoom)
                );

                float dx = p2.X - p1.X;
                float dy = p2.Y - p1.Y;
                float lengthSquared = dx * dx + dy * dy;

                float t = ((mouse.X - p1.X) * dx + (mouse.Y - p1.Y) * dy) / lengthSquared;
                t = Math.Clamp(t, 0, 1);

                float closestX = p1.X + t * dx;
                float closestY = p1.Y + t * dy;

                float dist = MathF.Sqrt(
                    (mouse.X - closestX) * (mouse.X - closestX) +
                    (mouse.Y - closestY) * (mouse.Y - closestY)
                );

                if (dist <= tolerance)
                    return edge;
            }
            return null;
        }




        private void InitNodeContextMenu()
        {
            _nodeMenu.Items.Clear();

            _nodeMenu.Items.Add("Edge Başlat", null, (s, e) =>
            {
                _edgeStartNode = _contextNode;
            });

            _nodeMenu.Items.Add("İsim Değiştir", null, (s, e) =>
            {
                if (_contextNode == null) return;

                string name = Microsoft.VisualBasic.Interaction.InputBox(
                    "Yeni node adı:",
                    "Node İsmi",
                    _contextNode.Name
                );

                if (!string.IsNullOrWhiteSpace(name))
                    _contextNode.Name = name;

                Invalidate();
            });

            _nodeMenu.Items.Add("Renk Değiştir", null, (s, e) =>
            {
                if (_contextNode == null) return;

                using var dlg = new ColorDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _contextNode.Color = dlg.Color;
                    Invalidate();
                }
            });
        }






        private void Form1_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (!_canvasRect.Contains(e.Location)) return;

            if (e.Delta > 0)
                _zoom += 0.1f;
            else
                _zoom -= 0.1f;

            _zoom = Math.Clamp(_zoom, 0.4f, 2.0f);
            Invalidate();
        }

        private void DeleteNode_Click(object? sender, EventArgs e)
        {
            if (_rightClickedNode == null || _graph == null) return;

            _graph.Edges.RemoveAll(e =>
                e.Source == _rightClickedNode ||
                e.Target == _rightClickedNode);

            _graph.Nodes.Remove(_rightClickedNode);

            _rightClickedNode = null;
            Invalidate();
        }


        private void NodeInfo_Click(object? sender, EventArgs e)
        {
            if (_rightClickedNode == null) return;

            MessageBox.Show(
                $"ID: {_rightClickedNode.Id}\n" +
                $"Name: {_rightClickedNode.Name}\n" +
                $"X: {_rightClickedNode.X}\n" +
                $"Y: {_rightClickedNode.Y}",
                "Node Bilgisi"
            );
        }

        private Node? GetNodeAt(Point mouse)
        {
            int r = (int)(30 * _zoom);

            foreach (var node in _graph!.Nodes)
            {
                var p = new Point(
                    _canvasRect.X + (int)(node.X * _zoom),
                    _canvasRect.Y + (int)(node.Y * _zoom)
                );

                var rect = new Rectangle(p.X, p.Y, r * 2, r * 2);
                if (rect.Contains(mouse))
                    return node;
            }

            return null;
        }

        private void ShowNodeMenu(Point location, Node node)
        {
            _nodeMenu.Items.Clear();

            // EDGE BAŞLAT
            _nodeMenu.Items.Add("Edge Başlat", null, (s, e) =>
            {
                _edgeStartNode = node;
            });

            // İSİM DEĞİŞTİR
            _nodeMenu.Items.Add("İsim Değiştir", null, (s, e) =>
            {
                string? name = Microsoft.VisualBasic.Interaction.InputBox(
                    "Yeni node adı:",
                    "Node İsmi",
                    node.Name
                );

                if (!string.IsNullOrWhiteSpace(name))
                {
                    node.Name = name;
                    Invalidate();
                }
            });

            // RENK DEĞİŞTİR
            _nodeMenu.Items.Add("Renk Değiştir", null, (s, e) =>
            {
                using var dlg = new ColorDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    node.Color = dlg.Color;
                    Invalidate();
                }
            });

            // 🔥 SADECE EDGE VARSA → EDGE SİL
            var edges = _graph!.Edges
                .Where(e => e.Source == node || e.Target == node)
                .ToList();

            if (edges.Count > 0)
            {
                _nodeMenu.Items.Add(new ToolStripSeparator());
                _nodeMenu.Items.Add("Edge Sil", null, (s, e) =>
                {
                    foreach (var edge in edges)
                        _graph.Edges.Remove(edge);

                    Invalidate();
                });
            }

            _nodeMenu.Show(this, location);
        }





        private void Form1_MouseDown(object? sender, MouseEventArgs e)
        {
            if (_graph == null) return;

            var node = GetNodeAt(e.Location);
            var edge = GetEdgeAt(e.Location);

            // SAĞ TIK → EDGE
            if (e.Button == MouseButtons.Right && edge != null)
            {
                _selectedEdge = edge;
                _edgeMenu.Show(this, e.Location);
                return;
            }

            // SAĞ TIK → NODE
            if (e.Button == MouseButtons.Right && node != null)
            {
                _contextNode = node;
                ShowNodeMenu(e.Location, node);
                return;
            }

            // SOL TIK
            if (e.Button == MouseButtons.Left && node != null)
            {
                // EDGE BİTİR
                if (_edgeStartNode != null && _edgeStartNode != node)
                {
                    _graph.Edges.Add(new Edge
                    {
                        Source = _edgeStartNode,
                        Target = node,
                        Weight = 1
                    });
                    _edgeStartNode = null;
                    Invalidate();
                    return;
                }

                // DRAG BAŞLAT
                _dragNode = node;
                _isDragging = true;

                var p = new Point(
                    _canvasRect.X + (int)(node.X * _zoom),
                    _canvasRect.Y + (int)(node.Y * _zoom)
                );
                _dragOffset = new Point(e.X - p.X, e.Y - p.Y);
            }
        }












        private void Form1_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_isDragging && _dragNode != null)
            {
                _dragNode.X = (int)((e.X - _dragOffset.X - _canvasRect.X) / _zoom);
                _dragNode.Y = (int)((e.Y - _dragOffset.Y - _canvasRect.Y) / _zoom);
                Invalidate();
            }
        }




        private void Form1_MouseUp(object? sender, MouseEventArgs e)
        {
            _isDragging = false;
            _dragNode = null;
        }






        private void btnCreateGraph(object sender, EventArgs e)
        {
            var n1 = new Node { Id = 1, Name = "A", X = 100, Y = 150 };
            var n2 = new Node { Id = 2, Name = "B", X = 250, Y = 150 };
            var n3 = new Node { Id = 3, Name = "C", X = 400, Y = 150 };


            _graph = new Graph();
            _graph.Nodes.AddRange(new[] { n1, n2, n3 });
            _graph.Edges.Add(new Edge { Source = n1, Target = n2, Weight = 1 });
            _graph.Edges.Add(new Edge { Source = n2, Target = n3, Weight = 1 });

            _activeNodes.Clear();
            _shortestPath.Clear();
            _degrees.Clear();
            _activeAlgorithm = "";
            Invalidate();
        }

        private void btnBFS(object sender, EventArgs e)
        {
            if (_graph == null) return;

            var sw = Stopwatch.StartNew();
            var bfs = new BFSAlgorithm();
            bfs.Execute(_graph, _graph.Nodes.First());
            sw.Stop();

            _activeNodes = bfs.VisitedNodes;
            _shortestPath.Clear();
            _topCentralNodes.Clear();

            _activeAlgorithm = "BFS";
            _lastAlgorithmTimeMs = sw.ElapsedMilliseconds;

            MessageBox.Show($"BFS süresi: {_lastAlgorithmTimeMs} ms");
            Invalidate();
        }


        private void btnDFS(object sender, EventArgs e)
        {
            if (_graph == null) return;

            var sw = Stopwatch.StartNew();
            var dfs = new DFSAlgorithm();
            dfs.Execute(_graph, _graph.Nodes.First());
            sw.Stop();

            _activeNodes = dfs.VisitedNodes;
            _shortestPath.Clear();
            _topCentralNodes.Clear();

            _activeAlgorithm = "DFS";
            _lastAlgorithmTimeMs = sw.ElapsedMilliseconds;

            MessageBox.Show($"DFS süresi: {_lastAlgorithmTimeMs} ms");
            Invalidate();
        }


        private void btnDijkstra_Click(object sender, EventArgs e)
        {
            if (_graph == null) return;

            var start = _graph.Nodes.First();
            var end = _graph.Nodes.Last();

            var sw = Stopwatch.StartNew();
            var dijkstra = new DijkstraAlgorithm();
            dijkstra.Execute(_graph, start);
            _shortestPath = dijkstra.GetShortestPath(start, end);
            sw.Stop();

            _activeNodes.Clear();
            _topCentralNodes.Clear();

            _activeAlgorithm = "DIJKSTRA";
            _lastAlgorithmTimeMs = sw.ElapsedMilliseconds;

            MessageBox.Show(
                "En kısa yol: " +
                string.Join(" -> ", _shortestPath.Select(n => n.Name)) +
                $"\nSüre: {_lastAlgorithmTimeMs} ms"
            );

            Invalidate();
        }


        private void btnCentrality(object sender, EventArgs e)
        {
            if (_graph == null) return;

            var sw = Stopwatch.StartNew();
            var calc = new CentralityCalculator();
            var result = calc.CalculateDegreeCentrality(_graph);
            sw.Stop();

            _topCentralNodes = result
                .OrderByDescending(x => x.Degree)
                .Take(5)
                .Select(x => x.Node)
                .ToList();

            _activeNodes.Clear();
            _shortestPath.Clear();

            _activeAlgorithm = "CENTRALITY";
            _lastAlgorithmTimeMs = sw.ElapsedMilliseconds;

            MessageBox.Show($"Degree Centrality süresi: {_lastAlgorithmTimeMs} ms");
            Invalidate();
        }




        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            if (_graph == null) return;

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // ===================== CANVAS =====================
            g.DrawRectangle(Pens.DarkGray, _canvasRect);
            g.SetClip(_canvasRect);

            int r = (int)(30 * _zoom);
            var positions = new Dictionary<Node, Point>();

            foreach (var node in _graph.Nodes)
            {
                positions[node] = new Point(
                    _canvasRect.X + (int)(node.X * _zoom),
                    _canvasRect.Y + (int)(node.Y * _zoom)
                );
            }

            // ===================== EDGES =====================
            if (_drawEdges)
            {
                foreach (var edge in _graph.Edges)
                {
                    var p1 = positions[edge.Source];
                    var p2 = positions[edge.Target];

                    Point a = new Point(p1.X + r, p1.Y + r);
                    Point b = new Point(p2.X + r, p2.Y + r);

                    g.DrawLine(Pens.Black, a, b);

                    // WEIGHT (ORTA NOKTA)
                    var mid = new Point(
                        (a.X + b.X) / 2,
                        (a.Y + b.Y) / 2
                    );

                    g.FillRectangle(Brushes.White, mid.X - 10, mid.Y - 8, 20, 16);
                    g.DrawString(
                        edge.Weight.ToString(),
                        Font,
                        Brushes.Black,
                        mid.X - 8,
                        mid.Y - 8
                    );
                }
            }

            // ===================== NODES =====================
            foreach (var node in _graph.Nodes)
            {
                var p = positions[node];

                // 🎯 ALGORİTMAYA GÖRE RENK
                Color fillColor = node.Color;

                if (_activeAlgorithm == "BFS" || _activeAlgorithm == "DFS")
                {
                    if (_activeNodes.Contains(node))
                        fillColor = Color.LightSkyBlue;
                }
                else if (_activeAlgorithm == "DIJKSTRA" || _activeAlgorithm == "ASTAR")
                {
                    if (_shortestPath.Contains(node))
                        fillColor = Color.LightGreen;
                }
                else if (_activeAlgorithm == "CENTRALITY")
                {
                    if (_topCentralNodes.Contains(node))
                        fillColor = Color.IndianRed;
                }

                using var brush = new SolidBrush(fillColor);
                g.FillEllipse(brush, p.X, p.Y, r * 2, r * 2);
                g.DrawEllipse(Pens.Black, p.X, p.Y, r * 2, r * 2);

                g.DrawString(
                    $"{node.Name}\nID:{node.Id}",
                    Font,
                    Brushes.Black,
                    p.X + r - 18,
                    p.Y + r - 12
                );
            }

            // ===================== ALGORİTMA SÜRESİ =====================
            if (!string.IsNullOrEmpty(_activeAlgorithm))
            {
                g.DrawString(
                    $"{_activeAlgorithm} Süre: {_lastAlgorithmTimeMs} ms",
                    Font,
                    Brushes.Black,
                    _canvasRect.X + 10,
                    _canvasRect.Y + 10
                );
            }

            g.ResetClip();
        }








        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnAStar_Click(object sender, EventArgs e)
        {
            if (_graph == null) return;

            var start = _graph.Nodes.First();
            var end = _graph.Nodes.Last();

            var sw = Stopwatch.StartNew();
            var astar = new AStarAlgorithm();
            _shortestPath = astar.FindPath(_graph, start, end);
            sw.Stop();

            _activeNodes.Clear();
            _topCentralNodes.Clear();

            _activeAlgorithm = "ASTAR";
            _lastAlgorithmTimeMs = sw.ElapsedMilliseconds;

            MessageBox.Show(
                "A* yolu: " +
                string.Join(" -> ", _shortestPath.Select(n => n.Name)) +
                $"\nSüre: {_lastAlgorithmTimeMs} ms"
            );

            Invalidate();
        }


        private void btnCloseness_Click(object sender, EventArgs e)
        {
            if (_graph == null) return;

            var sw = Stopwatch.StartNew();
            var calc = new CentralityCalculator();
            var result = calc.CalculateCloseness(_graph);
            sw.Stop();

            _centralityValues = result.ToDictionary(x => x.Node, x => x.Value);
            _activeAlgorithm = "CLOSENESS";

            MessageBox.Show(
                "Closeness Centrality:\n" +
                string.Join("\n",
                    result
                        .OrderByDescending(x => x.Value)
                        .Select(x => $"{x.Node.Name} → {x.Value:F3}")
                ) +
                $"\n\nSüre: {sw.ElapsedMilliseconds} ms"
            );

            Invalidate();
        }

        private void btnBetweenness_Click(object sender, EventArgs e)
        {
            if (_graph == null) return;

            var sw = Stopwatch.StartNew();
            var calc = new CentralityCalculator();
            var result = calc.CalculateBetweenness(_graph);
            sw.Stop();

            _centralityValues = result.ToDictionary(x => x.Node, x => x.Value);
            _activeAlgorithm = "BETWEENNESS";

            MessageBox.Show(
                "Betweenness Centrality:\n" +
                string.Join("\n",
                    result
                        .OrderByDescending(x => x.Value)
                        .Select(x => $"{x.Node.Name} → {x.Value}")
                ) +
                $"\n\nSüre: {sw.ElapsedMilliseconds} ms"
            );

            Invalidate();
        }

        private void btnRandomGraph_Click(object sender, EventArgs e)
        {
            int nodeCount = 10;
            int edgeCount = 20;

            _graph = GraphGenerator.GenerateRandomGraph(
                nodeCount,
                edgeCount,
                ClientSize.Width,
                ClientSize.Height
            );

            _activeNodes.Clear();
            _shortestPath.Clear();
            _centralityValues.Clear();
            _activeAlgorithm = "";

            Invalidate();
        }

        private void btnToggleEdges_Click(object sender, EventArgs e)
        {
            _drawEdges = !_drawEdges;
            Invalidate();
        }


        private void AddNode(string name)
        {
            if (_graph == null)
                _graph = new Graph();

            var node = new Node
            {
                Id = _nextNodeId++,
                Name = name,
                X = _rnd.Next(50, 500),
                Y = _rnd.Next(50, 300)
            };

            _graph.Nodes.Add(node);
            Invalidate();
        }

        private void btnAddNode_Click(object sender, EventArgs e)
        {
            string nodeName = $"N{_nextNodeId}";
            AddNode(nodeName);
        }

        private void btnAddNodeWithId_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtNodeId.Text);
            string name = txtNodeName.Text;

            if (_graph.Nodes.Any(n => n.Id == id))
            {
                MessageBox.Show("Bu ID zaten var!");
                return;
            }

            var node = new Node
            {
                Id = id,
                Name = name,
                X = _rnd.Next(50, 500),
                Y = _rnd.Next(50, 300)
            };

            _graph.Nodes.Add(node);
            _nextNodeId = Math.Max(_nextNodeId, id + 1);
            Invalidate();
        }

        private void AddEdge(int sourceId, int targetId, int weight = 1)
        {
            var source = _graph.Nodes.FirstOrDefault(n => n.Id == sourceId);
            var target = _graph.Nodes.FirstOrDefault(n => n.Id == targetId);

            if (source == null || target == null)
            {
                MessageBox.Show("Node bulunamadı");
                return;
            }

            _graph.Edges.Add(new Edge
            {
                Source = source,
                Target = target,
                Weight = weight
            });

            Invalidate();
        }

        private void btnAddEdge_Click(object sender, EventArgs e)
        {
            if (_graph == null) return;

            int from = int.Parse(txtEdgeFrom.Text);
            int to = int.Parse(txtEdgeTo.Text);

            AddEdge(from, to);
        }


    }
}
