using NodeMap.Core.Models;
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
        // -------------------------
        // DEGREE CENTRALITY
        // -------------------------
        public List<DegreeCentralityResult> CalculateDegreeCentrality(Graph graph)
        {
            return graph.Nodes.Select(n => new DegreeCentralityResult
            {
                Node = n,
                Degree = graph.Edges.Count(e =>
                    e.Source == n || e.Target == n)
            }).ToList();
        }

        // -------------------------
        // CLOSENESS CENTRALITY
        // -------------------------
        public List<CentralityResult> CalculateCloseness(Graph graph)
        {
            var results = new List<CentralityResult>();

            foreach (var node in graph.Nodes)
            {
                double totalDistance = 0;

                foreach (var target in graph.Nodes)
                {
                    if (node == target) continue;
                    var path = ShortestPath(graph, node, target);
                    if (path.Count > 0)
                        totalDistance += path.Count - 1;
                }

                results.Add(new CentralityResult
                {
                    Node = node,
                    Value = totalDistance > 0 ? 1.0 / totalDistance : 0
                });
            }

            return results;
        }

        // -------------------------
        // BETWEENNESS CENTRALITY
        // -------------------------
        public List<CentralityResult> CalculateBetweenness(Graph graph)
        {
            var scores = graph.Nodes.ToDictionary(n => n, n => 0.0);

            foreach (var s in graph.Nodes)
            {
                foreach (var t in graph.Nodes)
                {
                    if (s == t) continue;

                    var path = ShortestPath(graph, s, t);
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

        // -------------------------
        // BFS SHORTEST PATH
        // -------------------------
        private List<Node> ShortestPath(Graph graph, Node start, Node end)
        {
            var queue = new Queue<Node>();
            var prev = new Dictionary<Node, Node?>();

            queue.Enqueue(start);
            prev[start] = null;

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (current == end) break;

                foreach (var edge in graph.Edges.Where(e => e.Source == current))
                {
                    if (!prev.ContainsKey(edge.Target))
                    {
                        prev[edge.Target] = current;
                        queue.Enqueue(edge.Target);
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
