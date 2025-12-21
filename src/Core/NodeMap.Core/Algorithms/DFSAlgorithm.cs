using NodeMap.Core.Interfaces;
using NodeMap.Core.Models;

namespace NodeMap.Core.Algorithms
{
    public class DFSAlgorithm : IGraphAlgorithm
    {
        private HashSet<Node> _visited = new();

        public void Execute(Graph graph, Node start)
        {
            _visited.Clear();
            DFS(graph, start);
        }

        private void DFS(Graph graph, Node node)
        {
            _visited.Add(node);
            foreach (var neighbor in graph.GetNeighbors(node))
            {
                if (!_visited.Contains(neighbor))
                    DFS(graph, neighbor);
            }
        }
    }

}
