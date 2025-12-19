using NodeMap.Core.Models;
using System.Collections.Generic;

namespace NodeMap.Core.Algorithms
{
    public class ConnectedComponentsAlgorithm
    {
        public List<List<Node>> FindConnectedComponents(Graph graph)
        {
            var visited = new HashSet<Node>();
            var components = new List<List<Node>>();

            foreach (var node in graph.Nodes)
            {
                if (visited.Contains(node))
                    continue;

                var component = new List<Node>();
                BFS(graph, node, visited, component);
                components.Add(component);
            }

            return components;
        }

        private void BFS(Graph graph, Node start,
                         HashSet<Node> visited,
                         List<Node> component)
        {
            var queue = new Queue<Node>();
            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                component.Add(current);

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
