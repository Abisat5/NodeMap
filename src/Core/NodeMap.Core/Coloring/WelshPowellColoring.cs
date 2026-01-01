using NodeMap.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace NodeMap.Core.Coloring
{
    public class WelshPowellColoring
    {
        public Dictionary<Node, int> ColorGraph(Graph graph)
        {
            var result = new Dictionary<Node, int>();

            // düğümleri dereceye göre sıralama
            var sortedNodes = graph.Nodes
                .OrderByDescending(n => GetDegree(graph, n))
                .ToList();

            int currentColor = 1;

            foreach (var node in sortedNodes)
            {
                if (result.ContainsKey(node))
                    continue;

                result[node] = currentColor;

                foreach (var otherNode in sortedNodes)
                {
                    if (result.ContainsKey(otherNode))
                        continue;

                    if (CanUseColor(graph, result, otherNode, currentColor))
                    {
                        result[otherNode] = currentColor;
                    }
                }

                currentColor++;
            }

            return result;
        }

        // düğüm derecesi gösterme
        private int GetDegree(Graph graph, Node node)
        {
            return graph.Edges.Count(e =>
                e.Source.Equals(node) || e.Target.Equals(node));
        }

        // aynı renkte komşuluk kontrolü
        private bool CanUseColor(
            Graph graph,
            Dictionary<Node, int> colored,
            Node node,
            int color)
        {
            foreach (var edge in graph.Edges)
            {
                Node neighbor = null;

                if (edge.Source.Equals(node))
                    neighbor = edge.Target;
                else if (edge.Target.Equals(node))
                    neighbor = edge.Source;

                if (neighbor != null &&
                    colored.ContainsKey(neighbor) &&
                    colored[neighbor] == color)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
