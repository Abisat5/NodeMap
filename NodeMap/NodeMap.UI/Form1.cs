using NodeMap.Core.Models;
using NodeMap.Core.Algorithms;
using System.Drawing;
using System.Drawing.Text;

namespace NodeMap.UI
{
    public partial class Form1 : Form
    {
        private Graph _graph;
        public Form1()
        {
            
            InitializeComponent();
            this.Paint += Form1_Paint;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            var n1 = new Node { Id = 1, Name = "A" };
            var n2 = new Node { Id = 2, Name = "B" };
            var n3 = new Node { Id = 3, Name = "C" };

            var graph = new Graph();
            graph.Nodes.AddRange(new[] { n1, n2, n3 });

            graph.Edges.Add(new Edge { Source = n1, Target = n2, Weight = 1 });
            graph.Edges.Add(new Edge { Source = n2, Target = n3, Weight = 1 });

            _graph = graph;


            var bfs = new BFSAlgorithm();
            bfs.Execute(graph, n1);

            this.Invalidate();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (_graph == null) return;

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int x = 100;
            int y = 100;
            int radius = 30;

            var positions = new Dictionary<Node, Point>();


            foreach (var node in _graph.Nodes)
            {
                positions[node] = new Point(x, y);
                x += 120;
            }


            foreach (var edge in _graph.Edges)
            {
                var p1 = positions[edge.Source];
                var p2 = positions[edge.Target];

                g.DrawLine(
                    Pens.Black,
                    p1.X + radius, p1.Y + radius,
                    p2.X + radius, p2.Y + radius
                );
            }


            foreach (var node in _graph.Nodes)
            {
                var p = positions[node];

                g.FillEllipse(Brushes.LightBlue, p.X, p.Y, radius * 2, radius * 2);
                g.DrawEllipse(Pens.Black, p.X, p.Y, radius * 2, radius * 2);
                g.DrawString(node.Name, Font, Brushes.Black,
                    p.X + radius - 5, p.Y + radius - 7);
            }
        }


    }
}
