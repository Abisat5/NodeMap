using NodeMap.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NodeMap.Core.Algorithms
{
    public class DegreeCentralityResult
    {
        public Node Node { get; set; } = null!;
        public int Degree { get; set; }
    }

    public class CentralityResult
    {
        public Node Node { get; set; } = null!;
        public double Value { get; set; }
    }

    public class CentralityCalculator
    {


        // degree centrality
        public List<DegreeCentralityResult> CalculateDegreeCentrality(Graph graph)
        {
            return graph.Nodes.Select(n => new DegreeCentralityResult
            {
                Node = n,
                Degree = graph.Edges.Count(e =>
                    e.Source == n || e.Target == n)
            }).ToList();
        }

        
        // closeness centrality (DIJKSTRA ve WEIGHT)
         
        public List<CentralityResult> CalculateCloseness(Graph graph)
        {
            var results = new List<CentralityResult>();
            int nodeCount = graph.Nodes.Count;

            foreach (var node in graph.Nodes)
            {
                double totalDistance = 0;

                foreach (var target in graph.Nodes)
                {
                    if (node == target)
                        continue;

                    double distance = DijkstraDistance(graph, node, target);

                    if (distance > 0)
                        totalDistance += distance;
                }

                results.Add(new CentralityResult
                {
                    Node = node,
                    Value = totalDistance > 0
                        ? (nodeCount - 1) / totalDistance
                        : 0
                });
            }

            return results;
        }

        
        // betweenness centrality (DIJKSTRA PATH ve WEIGHT)
         
        public List<CentralityResult> CalculateBetweenness(Graph graph)
        {
            var scores = graph.Nodes.ToDictionary(n => n, n => 0.0);

            foreach (var source in graph.Nodes)
            {
                foreach (var target in graph.Nodes)
                {
                    if (source == target)
                        continue;

                    var path = DijkstraPath(graph, source, target);

                    if (path.Count < 3)
                        continue;

                    foreach (var middleNode in path.Skip(1).Take(path.Count - 2))
                    {
                        scores[middleNode]++;
                    }
                }
            }

            return scores.Select(x => new CentralityResult
            {
                Node = x.Key,
                Value = x.Value
            }).ToList();
        }

         
        // DIJKSTRA sadece mesafe (WEIGHT)
         
        private double DijkstraDistance(Graph graph, Node start, Node end)
        {
            var distances = graph.Nodes.ToDictionary(n => n, n => double.PositiveInfinity);
            var visited = new HashSet<Node>();

            distances[start] = 0;

            while (visited.Count < graph.Nodes.Count)
            {
                var current = distances
                    .Where(x => !visited.Contains(x.Key))
                    .OrderBy(x => x.Value)
                    .FirstOrDefault().Key;

                if (current == null || current == end)
                    break;

                visited.Add(current);

                foreach (var edge in graph.Edges)
                {
                    Node neighbor = null;

                    if (edge.Source == current)
                        neighbor = edge.Target;
                    else if (edge.Target == current)
                        neighbor = edge.Source;

                    if (neighbor == null || visited.Contains(neighbor))
                        continue;

                    double alternative = distances[current] + edge.Weight;

                    if (alternative < distances[neighbor])
                        distances[neighbor] = alternative;
                }
            }

            return distances[end] == double.PositiveInfinity ? 0 : distances[end];
        }

         
        // DIJKSTRA – yolu döndürür (WEIGHT)
         
        private List<Node> DijkstraPath(Graph graph, Node start, Node end)
        {
            var distances = graph.Nodes.ToDictionary(n => n, n => double.PositiveInfinity);
            var previous = new Dictionary<Node, Node?>();
            var visited = new HashSet<Node>();

            distances[start] = 0;
            previous[start] = null;

            while (visited.Count < graph.Nodes.Count)
            {
                var current = distances
                    .Where(x => !visited.Contains(x.Key))
                    .OrderBy(x => x.Value)
                    .FirstOrDefault().Key;

                if (current == null)
                    break;

                visited.Add(current);

                if (current == end)
                    break;

                foreach (var edge in graph.Edges)
                {
                    Node neighbor = null;

                    if (edge.Source == current)
                        neighbor = edge.Target;
                    else if (edge.Target == current)
                        neighbor = edge.Source;

                    if (neighbor == null || visited.Contains(neighbor))
                        continue;

                    double alternative = distances[current] + edge.Weight;

                    if (alternative < distances[neighbor])
                    {
                        distances[neighbor] = alternative;
                        previous[neighbor] = current;
                    }
                }
            }

            if (!previous.ContainsKey(end))
                return new List<Node>();

            var path = new List<Node>();
            for (var at = end; at != null; at = previous[at])
                path.Add(at);

            path.Reverse();
            return path;
        }
    }
}
