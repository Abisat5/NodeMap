using NodeMap.Core.Interfaces;
using NodeMap.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace NodeMap.Core.Algorithms
{
    public class DijkstraAlgorithm : IGraphAlgorithm
    {
        public Dictionary<Node, double> Distances { get; private set; } = new();

        public void Execute(Graph graph, Node start)
        {
            Distances = graph.Nodes.ToDictionary(n => n, n => double.MaxValue);
            Distances[start] = 0;

            var visited = new HashSet<Node>();

            while (visited.Count < graph.Nodes.Count)
            {
                var current = Distances
                    .Where(d => !visited.Contains(d.Key))
                    .OrderBy(d => d.Value)
                    .First().Key;

                visited.Add(current);

                foreach (var neighbor in graph.GetNeighbors(current))
                {
                    var edge = graph.GetEdge(current, neighbor);
                    if (edge == null) continue;

                    double newDist = Distances[current] + edge.Weight;
                    if (newDist < Distances[neighbor])
                        Distances[neighbor] = newDist;
                }
            }
        }
    }
}
