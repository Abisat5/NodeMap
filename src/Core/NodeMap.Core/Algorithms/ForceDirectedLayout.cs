using NodeMap.Core.Models;
using System;
using System.Collections.Generic;

namespace NodeMap.Core.Algorithms
{
    public static class ForceDirectedLayout
    {
        public static void Apply(
            Graph graph,
            int width,
            int height,
            int iterations = 200)
        {
            double area = width * height;
            double k = Math.Sqrt(area / graph.Nodes.Count);
            double temperature = width / 10.0;
            var rand = new Random();

            // Başlangıçta hafif random dağıt
            foreach (var n in graph.Nodes)
            {
                n.X = rand.Next(50, width - 50);
                n.Y = rand.Next(50, height - 50);
            }

            for (int i = 0; i < iterations; i++)
            {
                var disp = new Dictionary<Node, (double x, double y)>();

                foreach (var v in graph.Nodes)
                    disp[v] = (0, 0);

                // 🔴 Node-node itme
                foreach (var v in graph.Nodes)
                {
                    foreach (var u in graph.Nodes)
                    {
                        if (v == u) continue;

                        double dx = v.X - u.X;
                        double dy = v.Y - u.Y;
                        double dist = Math.Sqrt(dx * dx + dy * dy) + 0.01;

                        double force = (k * k) / dist;

                        disp[v] = (
                            disp[v].x + (dx / dist) * force,
                            disp[v].y + (dy / dist) * force
                        );
                    }
                }

                // 🟢 Edge çekme
                foreach (var e in graph.Edges)
                {
                    var v = e.Source;
                    var u = e.Target;

                    double dx = v.X - u.X;
                    double dy = v.Y - u.Y;
                    double dist = Math.Sqrt(dx * dx + dy * dy) + 0.01;

                    double force = (dist * dist) / k;

                    var fx = (dx / dist) * force;
                    var fy = (dy / dist) * force;

                    disp[v] = (disp[v].x - fx, disp[v].y - fy);
                    disp[u] = (disp[u].x + fx, disp[u].y + fy);
                }

                // 📍 Konum güncelle
                foreach (var v in graph.Nodes)
                {
                    double dx = disp[v].x;
                    double dy = disp[v].y;
                    double dispLength = Math.Sqrt(dx * dx + dy * dy);

                    if (dispLength > 0)
                    {
                        v.X += (int)((dx / dispLength) * Math.Min(dispLength, temperature));
                        v.Y += (int)((dy / dispLength) * Math.Min(dispLength, temperature));
                    }

                    // Canvas dışına çıkmasın
                    v.X = Math.Clamp(v.X, 30, width - 60);
                    v.Y = Math.Clamp(v.Y, 30, height - 60);
                }

                temperature *= 0.95; // soğut
            }
        }
    }
}
