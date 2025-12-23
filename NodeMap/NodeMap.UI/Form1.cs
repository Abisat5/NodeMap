using NodeMap.Core.Models;
using NodeMap.Core.Algorithms;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

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


        private Rectangle _canvasRect = new Rectangle(20, 20, 500, 300);




        public Form1()
        {
            InitializeComponent();
            this.Paint += Form1_Paint;
            this.MouseWheel += Form1_MouseWheel;
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
            _degrees.Clear();
            _activeAlgorithm = "BFS";

            MessageBox.Show($"BFS süresi: {sw.ElapsedMilliseconds} ms");
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
            _degrees.Clear();
            _activeAlgorithm = "DFS";

            MessageBox.Show($"DFS süresi: {sw.ElapsedMilliseconds} ms");
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
            _degrees.Clear();
            _activeAlgorithm = "DIJKSTRA";

            MessageBox.Show(
                "En kisa yol: " +
                string.Join(" -> ", _shortestPath.Select(n => n.Name)) +
                $"\nSüre: {sw.ElapsedMilliseconds} ms"
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

            _centrality = result.ToDictionary(x => x.Node, x => x.Degree);
            _topCentralNodes = result
                .OrderByDescending(x => x.Degree)
                .Take(5)
                .Select(x => x.Node)
                .ToList();

            _activeNodes.Clear();
            _shortestPath.Clear();
            _activeAlgorithm = "CENTRALITY";

            string msg =
                "Top Degree Centrality:\n" +
                string.Join("\n",
                    result
                    .OrderByDescending(x => x.Degree)
                    .Take(5)
                    .Select(x => $"{x.Node.Name} → Degree: {x.Degree}")
                ) +
                $"\n\nSüre: {sw.ElapsedMilliseconds} ms";

            MessageBox.Show(msg);
            Invalidate();
        }



        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            if (_graph == null) return;

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // ---- CANVAS ----
            g.DrawRectangle(Pens.DarkGray, _canvasRect);
            g.SetClip(_canvasRect);

            int baseR = (int)(30 * _zoom);
            var positions = new Dictionary<Node, Point>();

            foreach (var node in _graph.Nodes)
            {
                positions[node] = new Point(
                    _canvasRect.X + (int)(node.X * _zoom),
                    _canvasRect.Y + (int)(node.Y * _zoom)
                );
            }

            // ---- EDGES ----
            if (_drawEdges)
            {
                foreach (var edge in _graph.Edges)
                {
                    var p1 = positions[edge.Source];
                    var p2 = positions[edge.Target];

                    Pen pen =
                        _activeAlgorithm == "BFS" ? Pens.Red :
                        _activeAlgorithm == "DFS" ? Pens.Green :
                        _activeAlgorithm == "DIJKSTRA" ? Pens.Blue :
                        Pens.Black;

                    g.DrawLine(
                        pen,
                        p1.X + baseR,
                        p1.Y + baseR,
                        p2.X + baseR,
                        p2.Y + baseR
                    );
                }
            }

            // ---- NODES ----
            foreach (var node in _graph.Nodes)
            {
                var p = positions[node];

                Brush brush =
                    _activeAlgorithm is "CLOSENESS" or "BETWEENNESS"
                        ? Brushes.Gold
                    : _activeNodes.Contains(node) && _activeAlgorithm == "BFS"
                        ? Brushes.LightCoral
                    : _activeNodes.Contains(node) && _activeAlgorithm == "DFS"
                        ? Brushes.LightGreen
                    : Brushes.LightBlue;

                g.FillEllipse(brush, p.X, p.Y, baseR * 2, baseR * 2);
                g.DrawEllipse(Pens.Black, p.X, p.Y, baseR * 2, baseR * 2);

                g.DrawString(
                    node.Name,
                    Font,
                    Brushes.Black,
                    p.X + baseR - 5,
                    p.Y + baseR - 7
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

            MessageBox.Show(
                "A* yolu: " +
                string.Join(" -> ", _shortestPath.Select(n => n.Name)) +
                $"\nSüre: {sw.ElapsedMilliseconds} ms"
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

    }
}
