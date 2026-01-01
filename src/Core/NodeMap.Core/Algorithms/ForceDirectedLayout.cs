using NodeMap.Core.Models;
using System;
using System.Collections.Generic;
namespace NodeMap.Core.Algorithms
{
    // force-directed graph yerleşim algoritması
    public static class ForceDirectedLayout
    {
        // grafın düğümlerini kuvvet tabanlı yaklaşımla yeniden konumlandırır
        public static void Apply(
            Graph graph,
            int width,
            int height,
            int iterations = 200)
        {
            // toplam alan ve ideal düğüm mesafesi hesaplanır
            double area = width * height;
            double k = Math.Sqrt(area / graph.Nodes.Count);

            // başlangıç sıcaklığı (hareket miktarını sınırlar)
            double temperature = width / 10.0;
            var rand = new Random();

            // başlangıçta düğümler canvas içinde rastgele dağıtılır
            foreach (var n in graph.Nodes)
            {
                n.X = rand.Next(50, width - 50);
                n.Y = rand.Next(50, height - 50);
            }

            // iteratif kuvvet hesaplama döngüsü
            for (int i = 0; i < iterations; i++)
            {
                // her düğüm için yer değiştirme vektörü tutulur
                var disp = new Dictionary<Node, (double x, double y)>();
                foreach (var v in graph.Nodes)
                    disp[v] = (0, 0);

                // düğümler arası itme kuvvetleri
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

                // kenarlar boyunca çekme kuvvetleri
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

                // hesaplanan kuvvetlere göre düğüm konumları güncellenir
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

                    // düğümlerin canvas dışına çıkması engellenir
                    v.X = Math.Clamp(v.X, 30, width - 60);
                    v.Y = Math.Clamp(v.Y, 30, height - 60);
                }

                // sıcaklık düşürülür
                temperature *= 0.95;
            }
        }
    }
}
