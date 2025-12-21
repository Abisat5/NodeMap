using NodeMap.Core.Models;
using System.Collections.Generic;

namespace NodeMap.Core.Algorithms
{
    public class ConnectedComponentsAlgorithm
    {
        public List<List<Node>> Find(Graph graph)
        {
            var visited = new HashSet<Node>();
            var components = new List<List<Node>>();

            foreach (var node in graph.Nodes)
            {
                if (visited.Contains(node)) continue;

                var component = new List<Node>();
                DFS(graph, node, visited, component);
                components.Add(component);
            }
            return components;
        }

        private void DFS(Graph graph, Node node,
                         HashSet<Node> visited,
                         List<Node> component)
        {
            visited.Add(node);
            component.Add(node);

            foreach (var n in graph.GetNeighbors(node))
            {
                if (!visited.Contains(n))
                    DFS(graph, n, visited, component);
            }
        }
    }

}
