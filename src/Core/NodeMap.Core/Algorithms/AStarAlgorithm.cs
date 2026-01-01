using NodeMap.Core.Interfaces;
using NodeMap.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NodeMap.Core.Algorithms
{
    // a* en kýsa yol algoritmasýný temsil eden sýnýf
    public class AStarAlgorithm : IGraphAlgorithm
    {
        // düðümlerin hangi düðümden gelindiðini tutar
        private Dictionary<Node, Node> _cameFrom = new();

        // baþlangýçtan düðüme olan gerçek maliyet
        private Dictionary<Node, double> _gScore = new();

        // tahmini toplam maliyet (g + heuristic)
        private Dictionary<Node, double> _fScore = new();

        // algoritmanýn çalýþma süresi
        public long ElapsedMilliseconds { get; private set; }

        // arayüz gereði var (kullanýmda deðil)
        public void Execute(Graph graph, Node start)
        {

        }

        // baþlangýç ve hedef arasýndaki en kýsa yolu bulur
        public List<Node> FindPath(Graph graph, Node start, Node goal)
        {
            var sw = Stopwatch.StartNew();

            // deðerlendirilecek düðümler listesi
            var openSet = new List<Node> { start };

            // önceki çalýþmalardan kalan veriler temizlenir
            _cameFrom.Clear();
            _gScore.Clear();
            _fScore.Clear();

            // tüm düðümler için baþlangýç deðerleri atanýr
            foreach (var node in graph.Nodes)
            {
                _gScore[node] = double.MaxValue;
                _fScore[node] = double.MaxValue;
            }

            // baþlangýç düðümü ayarlanýr
            _gScore[start] = 0;
            _fScore[start] = Heuristic(start, goal);

            // açýk liste boþalana kadar devam eder
            while (openSet.Any())
            {
                // en düþük tahmini maliyetli düðüm seçilir
                var current = openSet.OrderBy(n => _fScore[n]).First();

                // hedefe ulaþýldýysa yol oluþturulur
                if (current == goal)
                {
                    sw.Stop();
                    ElapsedMilliseconds = sw.ElapsedMilliseconds;
                    return ReconstructPath(current);
                }

                openSet.Remove(current);

                // komþu düðümler kontrol edilir
                foreach (var neighbor in graph.GetNeighbors(current))
                {
                    var edge = graph.GetEdge(current, neighbor);
                    if (edge == null) continue;

                    // yeni maliyet hesaplanýr
                    double tentativeG = _gScore[current] + edge.Weight;

                    // daha iyi yol bulunduysa güncellenir
                    if (tentativeG < _gScore[neighbor])
                    {
                        _cameFrom[neighbor] = current;
                        _gScore[neighbor] = tentativeG;
                        _fScore[neighbor] =
                            tentativeG + Heuristic(neighbor, goal);

                        if (!openSet.Contains(neighbor))
                            openSet.Add(neighbor);
                    }
                }
            }

            // hedefe ulaþýlamadýysa boþ liste döner
            sw.Stop();
            ElapsedMilliseconds = sw.ElapsedMilliseconds;
            return new List<Node>();
        }

        // manhattan uzaklýðý ile maliyet hesaplar
        private double Heuristic(Node a, Node b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        // hedef düðümden geriye doðru yolu oluþturur
        private List<Node> ReconstructPath(Node current)
        {
            var path = new List<Node> { current };

            while (_cameFrom.ContainsKey(current))
            {
                current = _cameFrom[current];
                path.Add(current);
            }

            path.Reverse();
            return path;
        }
    }
}
