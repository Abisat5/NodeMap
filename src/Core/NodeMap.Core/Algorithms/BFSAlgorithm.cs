using NodeMap.Core.Models;
using NodeMap.Core.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;

namespace NodeMap.Core.Algorithms
{
    public class BFSAlgorithm : IGraphAlgorithm
    {
        public List<Node> VisitedNodes { get; private set; } = new();
        public long ElapsedMilliseconds { get; private set; }

        public void Execute(Graph graph, Node start)
        {
            var sw = Stopwatch.StartNew();

            VisitedNodes.Clear();
            var visited = new HashSet<Node>();
            var queue = new Queue<Node>();

            visited.Add(start);
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                VisitedNodes.Add(current);

                foreach (var neighbor in graph.GetNeighbors(current))
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }

            sw.Stop();
            ElapsedMilliseconds = sw.ElapsedMilliseconds;
        }
    }
}
