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

            // 1️⃣ Düğümleri dereceye göre sırala (azalan)
            var sortedNodes = graph.Nodes
                .OrderByDescending(n => GetDegree(graph, n))
                .ToList();

            int currentColor = 1;

            foreach (var node in sortedNodes)
            {
                if (result.ContainsKey(node))
                    continue;

                // 2️⃣ Düğüme yeni renk ata
                result[node] = currentColor;

                // 3️⃣ Aynı rengi alabilecek diğer düğümler
                foreach (var otherNode in sortedNodes)
                {
                    if (result.ContainsKey(otherNode))
                        continue;

                    if (!AreAdjacent(graph, node, otherNode) &&
                        CanUseColor(graph, result, otherNode, currentColor))
                    {
                        result[otherNode] = currentColor;
                    }
                }

                currentColor++;
            }

            return result;
        }

        // 🔹 İki düğüm komşu mu?
        private bool AreAdjacent(Graph graph, Node a, Node b)
        {
            return graph.Edges.Any(e =>
                (e.Source == a && e.Target == b) ||
                (e.Source == b && e.Target == a));
        }

        // 🔹 Düğümün derecesi
        private int GetDegree(Graph graph, Node node)
        {
            return graph.Edges.Count(e =>
                e.Source == node || e.Target == node);
        }

        // 🔹 Aynı renkte komşu var mı kontrolü
        private bool CanUseColor(
            Graph graph,
            Dictionary<Node, int> colored,
            Node node,
            int color)
        {
            foreach (var edge in graph.Edges)
            {
                Node neighbor = null;

                if (edge.Source == node)
                    neighbor = edge.Target;
                else if (edge.Target == node)
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
