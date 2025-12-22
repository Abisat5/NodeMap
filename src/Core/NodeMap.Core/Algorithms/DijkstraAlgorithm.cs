using NodeMap.Core.Interfaces;
using NodeMap.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace NodeMap.Core.Algorithms
{
    public class DijkstraAlgorithm : IGraphAlgorithm
    {
        public Dictionary<Node, double> Distances { get; private set; } = new();
        private Dictionary<Node, Node> _previous = new();

        public void Execute(Graph graph, Node start)
        {
            Distances = graph.Nodes.ToDictionary(n => n, n => double.MaxValue);
            _previous = new Dictionary<Node, Node>();

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
                    {
                        Distances[neighbor] = newDist;
                        _previous[neighbor] = current;
                    }
                }
            }
        }

        public List<Node> GetShortestPath(Node start, Node end)
        {
            var path = new List<Node>();
            var current = end;

            while (!current.Equals(start))
            {
                path.Add(current);
                if (!_previous.ContainsKey(current))
                    return new List<Node>();

                current = _previous[current];
            }

            path.Add(start);
            path.Reverse();
            return path;
        }
    }
}
