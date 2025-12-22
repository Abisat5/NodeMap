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

        public Form1()
        {
            InitializeComponent();
            this.Paint += Form1_Paint;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btnCreateGraph(object sender, EventArgs e)
        {
            var n1 = new Node { Id = 1, Name = "A" };
            var n2 = new Node { Id = 2, Name = "B" };
            var n3 = new Node { Id = 3, Name = "C" };

            _graph = new Graph();
            _graph.Nodes.AddRange(new[] { n1, n2, n3 });
            _graph.Edges.Add(new Edge { Source = n1, Target = n2, Weight = 1 });
            _graph.Edges.Add(new Edge { Source = n2, Target = n3, Weight = 1 });

            _activeNodes.Clear();
            _shortestPath.Clear();
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
            _activeAlgorithm = "DIJKSTRA";

            MessageBox.Show(
                "En kýsa yol: " +
                string.Join(" -> ", _shortestPath.Select(n => n.Name)) +
                $"\nSüre: {sw.ElapsedMilliseconds} ms"
            );

            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (_graph == null) return;

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int x = 120;
            int y = 150;
            int r = 30;

            var positions = new Dictionary<Node, Point>();

            foreach (var node in _graph.Nodes)
            {
                positions[node] = new Point(x, y);
                x += 150;
            }

            foreach (var edge in _graph.Edges)
            {
                var p1 = positions[edge.Source];
                var p2 = positions[edge.Target];

                bool inShortestPath =
                    _shortestPath.Count > 1 &&
                    _shortestPath.Contains(edge.Source) &&
                    _shortestPath.Contains(edge.Target) &&
                    Math.Abs(
                        _shortestPath.IndexOf(edge.Source) -
                        _shortestPath.IndexOf(edge.Target)
                    ) == 1;

                bool activeTraversal =
                    _activeNodes.Contains(edge.Source) &&
                    _activeNodes.Contains(edge.Target);

                Pen pen =
                    inShortestPath ? Pens.Blue :
                    activeTraversal && _activeAlgorithm == "BFS" ? Pens.Red :
                    activeTraversal && _activeAlgorithm == "DFS" ? Pens.Green :
                    Pens.Black;

                g.DrawLine(
                    pen,
                    p1.X + r, p1.Y + r,
                    p2.X + r, p2.Y + r
                );
            }

            foreach (var node in _graph.Nodes)
            {
                var p = positions[node];

                Brush brush =
                    _shortestPath.Contains(node) ? Brushes.LightSkyBlue :
                    _activeNodes.Contains(node) && _activeAlgorithm == "BFS" ? Brushes.LightCoral :
                    _activeNodes.Contains(node) && _activeAlgorithm == "DFS" ? Brushes.LightGreen :
                    Brushes.LightBlue;

                g.FillEllipse(brush, p.X, p.Y, r * 2, r * 2);
                g.DrawEllipse(Pens.Black, p.X, p.Y, r * 2, r * 2);

                g.DrawString(
                    node.Name,
                    Font,
                    Brushes.Black,
                    p.X + r - 5,
                    p.Y + r - 7
                );
            }
        }
    }
}
