using NodeMap.Core.Models;
using NodeMap.Core.Algorithms;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Windows.Forms.VisualStyles;
using NodeMap.Core.IO;
using NodeMap.Core.Utils;

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


        
        private bool _highlightShortestPath = false;



        private ContextMenuStrip _edgeMenu = new();

        private Node? _selectedNode;
        private ContextMenuStrip _nodeMenu = new();

        private Node? _contextNode;
        private Node? _dragNode;
        private Node? _edgeStartNode;
        private bool _isDragging;
        private Point _dragOffset;

        private Edge? _selectedEdge = null;

        private double _lastAlgorithmTimeMs = 0;



        private bool _manualNodeMode = false;
        private Node? _lastCreatedNode = null;



        // ================= NODE INFO PANEL =================
        private Panel pnlNodeInfo;
        private TextBox txtNodeSearchId;
        private Label lblNodeName;
        private Label lblNodeDegree;
        private Label lblNodeStatus;
        private Label lblNodeActivity;










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

            btnManualNodeMode = new Button();
            btnManualNodeMode.Location = new Point(1291, 271);
            btnManualNodeMode.Size = new Size(140, 40);
            btnManualNodeMode.Text = "Manuel Node Modu";
            btnManualNodeMode.Click += btnManualNodeMode_Click;
            Controls.Add(btnManualNodeMode);
            InitNodeInfoPanel();




        }



        private void FillNodeInfo(Node node)
        {
            if (_graph == null) return;

            txtNodeSearchId.Text = node.Id.ToString();

            int degree = _graph.Edges.Count(e =>
                e.Source == node || e.Target == node);

            lblNodeName.Text = $"İsim: {node.Name}";
            lblNodeDegree.Text = $"Bağlantı: {degree}";

            // Durum
            if (_shortestPath.Contains(node))
                lblNodeStatus.Text = "Durum: Shortest Path";
            else if (_topCentralNodes.Contains(node))
                lblNodeStatus.Text = "Durum: Central";
            else if (_activeNodes.Contains(node))
                lblNodeStatus.Text = "Durum: Aktif";
            else
                lblNodeStatus.Text = "Durum: Normal";

            lblNodeActivity.Text = $"Aktivite: {_activeAlgorithm}";
        }





        private void InitNodeInfoPanel()
        {
            pnlNodeInfo = new Panel();
            pnlNodeInfo.Location = new Point(1291, 350);
            pnlNodeInfo.Size = new Size(230, 230);
            pnlNodeInfo.BackColor = Color.FromArgb(245, 247, 250);
            pnlNodeInfo.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(pnlNodeInfo);

            var title = new Label
            {
                Text = "Node Bilgi Paneli",
                Font = new Font(Font, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };
            pnlNodeInfo.Controls.Add(title);

            pnlNodeInfo.Controls.Add(new Label
            {
                Text = "Node ID:",
                Location = new Point(10, 45),
                AutoSize = true
            });

            txtNodeSearchId = new TextBox
            {
                Location = new Point(80, 42),
                Width = 60
            };
            pnlNodeInfo.Controls.Add(txtNodeSearchId);

            var btnFind = new Button
            {
                Text = "Getir",
                Location = new Point(150, 40),
                Width = 70
            };
            btnFind.Click += BtnFindNode_Click;
            pnlNodeInfo.Controls.Add(btnFind);

            lblNodeName = CreateInfoLabel("İsim:", 80);
            lblNodeDegree = CreateInfoLabel("Bağlantı:", 105);
            lblNodeStatus = CreateInfoLabel("Durum:", 130);
            lblNodeActivity = CreateInfoLabel("Aktivite:", 155);
        }


        private void BtnFindNode_Click(object? sender, EventArgs e)
        {
            if (_graph == null) return;

            if (!int.TryParse(txtNodeSearchId.Text, out int id))
                return;

            var node = _graph.Nodes.FirstOrDefault(n => n.Id == id);
            if (node == null)
            {
                MessageBox.Show("Node bulunamadı");
                return;
            }

            FillNodeInfo(node);
        }



        private Label CreateInfoLabel(string text, int y)
        {
            var lbl = new Label
            {
                Text = text,
                Location = new Point(10, y),
                AutoSize = true
            };
            pnlNodeInfo.Controls.Add(lbl);
            return lbl;
        }










        private Button btnManualNodeMode;

        private void btnManualNodeMode_Click(object sender, EventArgs e)
        {
            _manualNodeMode = !_manualNodeMode;
            btnManualNodeMode.BackColor =
                _manualNodeMode ? Color.LightGreen : SystemColors.Control;
        }

        private string GetNextNodeName()
        {
            int index = _graph.Nodes.Count;
            return ((char)('A' + index)).ToString();
        }


        private Color GetGradientColor(int index, int total, bool isDFS)
        {
            if (total <= 1) return Color.LightSkyBlue;

            float t = index / (float)(total - 1);

            if (isDFS)
            {
                // DFS → mor tonları
                int r = (int)(180 + 50 * t);
                int g = (int)(120 * (1 - t));
                int b = 220;
                return Color.FromArgb(r, g, b);
            }
            else
            {
                // BFS → mavi-yeşil geçiş
                int r = (int)(100 * (1 - t));
                int g = (int)(180 + 40 * t);
                int b = 255;
                return Color.FromArgb(r, g, b);
            }
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

            // ===================== NODE SİL =====================
            _nodeMenu.Items.Add(new ToolStripSeparator());
            _nodeMenu.Items.Add("Node Sil", null, (s, e) =>
            {
                if (_graph == null) return;

                // Node'a bağlı tüm edge'leri sil
                _graph.Edges.RemoveAll(edge =>
                    edge.Source == node || edge.Target == node);

                // Node'u sil
                _graph.Nodes.Remove(node);

                // Eğer edge başlatma durumundaysa temizle
                if (_edgeStartNode == node)
                    _edgeStartNode = null;

                Invalidate();
            });

        }





        private void Form1_MouseDown(object? sender, MouseEventArgs e)
        {
            if (_graph == null) return;

            var node = GetNodeAt(e.Location);
            var edge = GetEdgeAt(e.Location);

            // ==================================================
            // MANUEL NODE EKLEME (SOL TIK + BOŞ ALAN)
            // ==================================================
            if (_manualNodeMode &&
                e.Button == MouseButtons.Left &&
                _canvasRect.Contains(e.Location) &&
                node == null)
            {
                var newNode = new Node
                {
                    Id = _nextNodeId++,
                    Name = GetNextNodeName(),
                    X = (int)((e.X - _canvasRect.X) / _zoom),
                    Y = (int)((e.Y - _canvasRect.Y) / _zoom),
                    Color = Color.LightGray
                };

                _graph.Nodes.Add(newNode);

                if (_lastCreatedNode != null)
                {
                    _graph.Edges.Add(new Edge
                    {
                        Source = _lastCreatedNode,
                        Target = newNode,
                        Weight = 1
                    });
                }

                _lastCreatedNode = newNode;
                Invalidate();
                return;
            }

            // ==================================================
            // SAĞ TIK → EDGE MENÜ
            // ==================================================
            if (e.Button == MouseButtons.Right && edge != null)
            {
                _selectedEdge = edge;
                _edgeMenu.Show(this, e.Location);
                return;
            }

            // ==================================================
            // SAĞ TIK → NODE MENÜ
            // ==================================================
            if (e.Button == MouseButtons.Right && node != null)
            {
                _contextNode = node;
                ShowNodeMenu(e.Location, node);
                return;
            }

            // ==================================================
            // SOL TIK → NODE SEÇİM / GLOW / DRAG
            // ==================================================
            if (e.Button == MouseButtons.Left && node != null)
            {
                // 🔥 GLOW İÇİN SEÇİLEN NODE
                _selectedNode = node;

                // 🔹 Node info paneli doldur
                FillNodeInfo(node);

                // 🔹 Edge tamamlama
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

                // 🔹 Drag başlat
                _dragNode = node;
                _isDragging = true;

                var p = new Point(
                    _canvasRect.X + (int)(node.X * _zoom),
                    _canvasRect.Y + (int)(node.Y * _zoom)
                );

                _dragOffset = new Point(
                    e.X - p.X,
                    e.Y - p.Y
                );

                Invalidate();
                return;
            }

            // ==================================================
            // SOL TIK → BOŞ ALAN (SEÇİM TEMİZLE)
            // ==================================================
            if (e.Button == MouseButtons.Left)
            {
                _selectedNode = null;
                Invalidate();
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
            _lastAlgorithmTimeMs = sw.Elapsed.TotalMilliseconds;


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
            _lastAlgorithmTimeMs = sw.Elapsed.TotalMilliseconds;
            ;

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
            _lastAlgorithmTimeMs = sw.Elapsed.TotalMilliseconds;


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
            _lastAlgorithmTimeMs = sw.Elapsed.TotalMilliseconds;
            ;

            MessageBox.Show($"Degree Centrality süresi: {_lastAlgorithmTimeMs} ms");
            Invalidate();
        }




        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            if (_graph == null) return;

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // ===================== CANVAS =====================
            using (var bgBrush = new SolidBrush(Color.FromArgb(245, 247, 250)))
                g.FillRectangle(bgBrush, _canvasRect);

            using (var canvasPen = new Pen(Color.Gray, 2))
                g.DrawRectangle(canvasPen, _canvasRect);

            g.SetClip(_canvasRect);

            int r = (int)(28 * _zoom);

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

                    Pen edgePen = new Pen(Color.DimGray, 2);

                    // 🔥 SADECE kullanıcı isterse ve uygun algoritma ise vurgula
                    if (_highlightShortestPath &&
                        (_activeAlgorithm == "DIJKSTRA" || _activeAlgorithm == "ASTAR") &&
                        _shortestPath.Count > 1)
                    {
                        for (int i = 0; i < _shortestPath.Count - 1; i++)
                        {
                            if (
                                (_shortestPath[i] == edge.Source && _shortestPath[i + 1] == edge.Target) ||
                                (_shortestPath[i] == edge.Target && _shortestPath[i + 1] == edge.Source)
                            )
                            {
                                edgePen = new Pen(Color.DarkGreen, 4);
                                break;
                            }
                        }
                    }

                    g.DrawLine(edgePen, a, b);
                }
            }


            // ===================== NODES =====================
            foreach (var node in _graph.Nodes)
            {
                var p = positions[node];
                Color fillColor = node.Color;
                Pen borderPen = new Pen(Color.Black, 2);

                // ================= ALGORITHM STATES =================
                if ((_activeAlgorithm == "BFS" || _activeAlgorithm == "DFS") &&
                    _activeNodes.Contains(node))
                {
                    int index = _activeNodes.IndexOf(node);
                    int total = Math.Max(1, _activeNodes.Count);
                    int blue = 180 + (int)(70.0 * index / total);
                    fillColor = Color.FromArgb(90, 140, blue);
                    borderPen = new Pen(Color.DarkBlue, 3);
                }
                else if ((_activeAlgorithm == "DIJKSTRA" || _activeAlgorithm == "ASTAR") &&
                         _shortestPath.Contains(node))
                {
                    fillColor = Color.LightGreen;
                    borderPen = new Pen(Color.DarkGreen, 3);
                }
                else if (_activeAlgorithm == "CENTRALITY" &&
                         _topCentralNodes.Contains(node))
                {
                    fillColor = Color.IndianRed;
                    borderPen = new Pen(Color.DarkRed, 3);
                }

                // ================= 🔥 GOLD GLOW (SELECTED NODE) =================
                if (_selectedNode == node)
                {
                    using (var glowBrush = new SolidBrush(Color.FromArgb(80, 255, 215, 0)))
                        g.FillEllipse(
                            glowBrush,
                            p.X - 10,
                            p.Y - 10,
                            r * 2 + 20,
                            r * 2 + 20
                        );

                    using (var glowPen = new Pen(Color.Gold, 4))
                        g.DrawEllipse(
                            glowPen,
                            p.X - 6,
                            p.Y - 6,
                            r * 2 + 12,
                            r * 2 + 12
                        );
                }

                // ================= SHADOW =================
                using (var shadowBrush = new SolidBrush(Color.FromArgb(60, Color.Black)))
                    g.FillEllipse(shadowBrush, p.X + 4, p.Y + 4, r * 2, r * 2);

                // ================= NODE BODY =================
                using (var brush = new SolidBrush(fillColor))
                    g.FillEllipse(brush, p.X, p.Y, r * 2, r * 2);

                g.DrawEllipse(borderPen, p.X, p.Y, r * 2, r * 2);

                // ================= NODE TEXT =================
                var textSize = g.MeasureString(node.Name, Font);
                g.DrawString(
                    node.Name,
                    Font,
                    Brushes.Black,
                    p.X + r - textSize.Width / 2,
                    p.Y + r - textSize.Height / 2
                );
            }


            // ===================== ALGORITHM INFO PANEL =====================
            if (!string.IsNullOrEmpty(_activeAlgorithm))
            {
                Rectangle panel = new Rectangle(
                    _canvasRect.X + 10,
                    _canvasRect.Y + 10,
                    240,
                    45
                );

                using (var panelBrush = new SolidBrush(Color.FromArgb(230, 255, 255, 255)))
                    g.FillRectangle(panelBrush, panel);

                g.DrawRectangle(Pens.Gray, panel);

                g.DrawString(
                    $"{_activeAlgorithm} • {_lastAlgorithmTimeMs:F4} ms",
                    Font,
                    Brushes.Black,
                    panel.X + 10,
                    panel.Y + 12
                );
            }

            // ===================== LEGEND PANEL =====================
            Rectangle legend = new Rectangle(
                _canvasRect.Right - 200,
                _canvasRect.Y + 10,
                190,
                120
            );

            using (var legendBrush = new SolidBrush(Color.FromArgb(235, 255, 255, 255)))
                g.FillRectangle(legendBrush, legend);

            g.DrawRectangle(Pens.Gray, legend);

            int ly = legend.Y + 10;

            void LegendItem(Color c, string text)
            {
                g.FillEllipse(new SolidBrush(c), legend.X + 10, ly, 16, 16);
                g.DrawEllipse(Pens.Black, legend.X + 10, ly, 16, 16);
                g.DrawString(text, Font, Brushes.Black, legend.X + 35, ly - 1);
                ly += 22;
            }

            LegendItem(Color.LightGray, "Normal Node");
            LegendItem(Color.LightSkyBlue, "BFS / DFS");
            LegendItem(Color.LightGreen, "Shortest Path");
            LegendItem(Color.IndianRed, "Central Node");

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
            _lastAlgorithmTimeMs = sw.Elapsed.TotalMilliseconds;


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
                $"\n\nSüre: {sw.Elapsed.TotalMilliseconds} ms"
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
                $"\n\nSüre: {sw.Elapsed.TotalMilliseconds} ms"
            );

            Invalidate();
        }

        private void btnRandomGraph_Click(object sender, EventArgs e)
        {
            int nodeCount = 15;
            int edgeCount = 25;

            _graph = GraphGenerator.GenerateRandomGraph(
                nodeCount,
                edgeCount,
                _canvasRect.Width,
                _canvasRect.Height
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

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog { Filter = "CSV|*.csv" };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            var exporter = new CsvGraphExporter();
            exporter.Export(_graph, sfd.FileName);
        }

        private void btnExportJson_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog { Filter = "JSON|*.json" };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            var exporter = new JsonGraphExporter();
            exporter.Export(_graph, sfd.FileName);
        }

        private void btnAdjList_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog { Filter = "CSV|*.csv" };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            var exporter = new CsvAdjacencyListExporter();
            exporter.Export(_graph, sfd.FileName);
        }

        private void btnAdjMatrix_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog { Filter = "CSV|*.csv" };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            var exporter = new CsvAdjacencyMatrixExporter();
            exporter.Export(_graph, sfd.FileName);
        }

        private void btnImportCsv_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = "CSV|*.csv" };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            var importer = new CsvGraphImporter();
            _graph = importer.Import(ofd.FileName);

            Invalidate(); // yeniden çiz
        }

        private void btnImportJson_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = "JSON|*.json" };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            var importer = new JsonGraphImporter();
            _graph = importer.Import(ofd.FileName);

            Invalidate();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            _graph = GraphSnapshot.Restore();
            Invalidate();
        }

    }
}
