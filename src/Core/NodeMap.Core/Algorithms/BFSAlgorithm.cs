using NodeMap.Core.Models;
using System.Collections.Generic;

namespace NodeMap.Core.Algorithms
{
    public class BFSAlgorithm
    {
        public List<Node> VisitedNodes { get; private set; } = new();

        public void Execute(Graph graph, Node start)
        {
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
        }
    }
}
