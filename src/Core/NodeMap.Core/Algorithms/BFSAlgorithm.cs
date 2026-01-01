
using NodeMap.Core.Models;
using NodeMap.Core.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;

namespace NodeMap.Core.Algorithms
{
    // breadth-first search algoritmasýný temsil eder
    public class BFSAlgorithm : IGraphAlgorithm
    {
        // ziyaret edilen düðümlerin sýrasýný tutar
        public List<Node> VisitedNodes { get; private set; } = new();

        // algoritmanýn çalýþma süresini milisaniye cinsinden tutar
        public long ElapsedMilliseconds { get; private set; }

        
        public void Execute(Graph graph, Node start)
        {
            // süre ölçümü baþlatýlýr
            var sw = Stopwatch.StartNew();

            // önceki çalýþmadan kalan veriler temizlenir
            VisitedNodes.Clear();

            // ziyaret edilen düðümleri kontrol etmek için küme
            var visited = new HashSet<Node>();

            // bfs için kuyruk yapýsý
            var queue = new Queue<Node>();

            // baþlangýç düðümü iþaretlenir ve kuyruða eklenir
            visited.Add(start);
            queue.Enqueue(start);

            // kuyruk boþalana kadar devam edilir
            while (queue.Count > 0)
            {
                // sýradaki düðüm kuyruktan alýnýr
                var current = queue.Dequeue();

                // düðüm ziyaret listesine eklenir
                VisitedNodes.Add(current);

                // komþu düðümler dolaþýlýr
                foreach (var neighbor in graph.GetNeighbors(current))
                {
                    // daha önce ziyaret edilmemiþse kuyruða eklenir
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }

            // süre ölçümü durur ve kaydedilir
            sw.Stop();
            ElapsedMilliseconds = sw.ElapsedMilliseconds;
        }
    }
}
