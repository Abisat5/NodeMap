using NodeMap.Core.Interfaces;
using NodeMap.Core.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NodeMap.Core.Algorithms
{
    // dijkstra en kýsa yol algoritmasý
    public class DijkstraAlgorithm : IGraphAlgorithm
    {
        // her düðüm için hesaplanan en kýsa mesafeleri tutar
        public Dictionary<Node, double> Distances { get; private set; } = new();

        // en kýsa yol oluþturmak için önceki düðüm bilgilerini saklar
        private Dictionary<Node, Node> _previous = new();

        // algoritmanýn çalýþma süresini milisaniye cinsinden tutar
        public long ElapsedMilliseconds { get; private set; }

        // algoritmayý verilen baþlangýç düðümünden çalýþtýrýr
        public void Execute(Graph graph, Node start)
        {
            // süre ölçümünü baþlatýr
            var sw = Stopwatch.StartNew();

            // tüm düðümler için baþlangýçta mesafeleri maksimum yapar
            Distances = graph.Nodes.ToDictionary(n => n, n => double.MaxValue);

            // önceki düðüm kayýtlarýný temizler
            _previous.Clear();

            // baþlangýç düðümünün mesafesi sýfýr yapýlýr
            Distances[start] = 0;

            // ziyaret edilen düðümleri takip eder
            var visited = new HashSet<Node>();

            // tüm düðümler ziyaret edilene kadar devam eder
            while (visited.Count < graph.Nodes.Count)
            {
                // ziyaret edilmemiþ düðümler arasýndan en kýsa mesafeli olaný seçer
                var current = Distances
                    .Where(d => !visited.Contains(d.Key))
                    .OrderBy(d => d.Value)
                    .First().Key;

                // seçilen düðümü ziyaret edildi olarak iþaretler
                visited.Add(current);

                // komþu düðümler üzerinden mesafeleri günceller
                foreach (var neighbor in graph.GetNeighbors(current))
                {
                    var edge = graph.GetEdge(current, neighbor);
                    if (edge == null) continue;

                    double newDist = Distances[current] + edge.Weight;

                    // daha kýsa bir yol bulunduysa güncelleme yapar
                    if (newDist < Distances[neighbor])
                    {
                        Distances[neighbor] = newDist;
                        _previous[neighbor] = current;
                    }
                }
            }

            // süre ölçümünü durdurur
            sw.Stop();
            ElapsedMilliseconds = sw.ElapsedMilliseconds;
        }

        // baþlangýç ve hedef düðüm arasýndaki en kýsa yolu döndürür
        public List<Node> GetShortestPath(Node start, Node end)
        {
            var path = new List<Node>();
            var current = end;

            // hedef düðümden baþlayarak geriye doðru yolu oluþturur
            while (!current.Equals(start))
            {
                path.Add(current);

                // yol yoksa boþ liste döner
                if (!_previous.ContainsKey(current))
                    return new List<Node>();

                current = _previous[current];
            }

            // baþlangýç düðümünü ekler
            path.Add(start);

            // yolu doðru sýraya çevirir
            path.Reverse();
            return path;
        }
    }
}
