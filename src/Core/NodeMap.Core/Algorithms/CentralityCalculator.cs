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
        // ======================================================
        // DEGREE CENTRALITY
        // ======================================================
        public List<DegreeCentralityResult> CalculateDegreeCentrality(Graph graph)
        {
            return graph.Nodes.Select(n => new DegreeCentralityResult
            {
                Node = n,
                Degree = graph.Edges.Count(e =>
                    e.Source == n || e.Target == n)
            }).ToList();
        }

        // ======================================================
        // CLOSENESS CENTRALITY (WEIGHTED – DIJKSTRA)
        // ======================================================
        public List<CentralityResult> CalculateCloseness(Graph graph)
        {
            var results = new List<CentralityResult>();
            int n = graph.Nodes.Count;

            foreach (var node in graph.Nodes)
            {
                double totalDistance = 0;

                foreach (var target in graph.Nodes)
                {
                    if (node == target) continue;

                    double dist = DijkstraDistance(graph, node, target);
                    if (dist > 0)
                        totalDistance += dist;
                }

                results.Add(new CentralityResult
                {
                    Node = node,
                    Value = totalDistance > 0
                        ? (n - 1) / totalDistance
                        : 0
                });
            }

            return results;
        }

        // ======================================================
        // BETWEENNESS CENTRALITY (WEIGHTED – DIJKSTRA PATH)
        // ======================================================
        public List<CentralityResult> CalculateBetweenness(Graph graph)
        {
            var scores = graph.Nodes.ToDictionary(n => n, _ => 0.0);
            var nodes = graph.Nodes;

            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = i + 1; j < nodes.Count; j++)
                {
                    var s = nodes[i];
                    var t = nodes[j];

                    var path = DijkstraPath(graph, s, t);
                    if (path.Count < 3) continue;

                    foreach (var v in path.Skip(1).Take(path.Count - 2))
                        scores[v]++;
                }
            }

            return scores.Select(x => new CentralityResult
            {
                Node = x.Key,
                Value = x.Value
            }).ToList();
        }

        // ======================================================
        // DIJKSTRA – SADECE MESAFE
        // ======================================================
        private double DijkstraDistance(Graph graph, Node start, Node end)
        {
            var dist = graph.Nodes.ToDictionary(n => n, _ => double.PositiveInfinity);
            var visited = new HashSet<Node>();

            dist[start] = 0;

            while (visited.Count < graph.Nodes.Count)
            {
                var current = dist
                    .Where(x => !visited.Contains(x.Key))
                    .OrderBy(x => x.Value)
                    .FirstOrDefault().Key;

                if (current == null || current == end)
                    break;

                visited.Add(current);

                foreach (var edge in graph.Edges
                    .Where(e => e.Source == current || e.Target == current))
                {
                    var neighbor = edge.Source == current ? edge.Target : edge.Source;
                    if (visited.Contains(neighbor)) continue;

                    double alt = dist[current] + edge.Weight;
                    if (alt < dist[neighbor])
                        dist[neighbor] = alt;
                }
            }

            return dist[end] == double.PositiveInfinity ? 0 : dist[end];
        }

        // ======================================================
        // DIJKSTRA – YOLU DÖNDÜRÜR
        // ======================================================
        private List<Node> DijkstraPath(Graph graph, Node start, Node end)
        {
            var dist = graph.Nodes.ToDictionary(n => n, _ => double.PositiveInfinity);
            var prev = new Dictionary<Node, Node?>();
            var visited = new HashSet<Node>();

            dist[start] = 0;
            prev[start] = null;

            while (visited.Count < graph.Nodes.Count)
            {
                var current = dist
                    .Where(x => !visited.Contains(x.Key))
                    .OrderBy(x => x.Value)
                    .FirstOrDefault().Key;

                if (current == null)
                    break;

                visited.Add(current);
                if (current == end)
                    break;

                foreach (var edge in graph.Edges
                    .Where(e => e.Source == current || e.Target == current))
                {
                    var neighbor = edge.Source == current ? edge.Target : edge.Source;
                    if (visited.Contains(neighbor)) continue;

                    double alt = dist[current] + edge.Weight;
                    if (alt < dist[neighbor])
                    {
                        dist[neighbor] = alt;
                        prev[neighbor] = current;
                    }
                }
            }

            if (!prev.ContainsKey(end))
                return new List<Node>();

            var path = new List<Node>();
            for (var at = end; at != null; at = prev[at])
                path.Add(at);

            path.Reverse();
            return path;
        }
    }
}
