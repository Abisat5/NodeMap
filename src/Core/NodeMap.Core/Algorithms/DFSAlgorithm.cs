using NodeMap.Core.Models;
using NodeMap.Core.Interfaces;
using System.Collections.Generic;

namespace NodeMap.Core.Algorithms
{
    public class DFSAlgorithm : IGraphAlgorithm
    {
        public List<Node> VisitedNodes { get; private set; } = new();

        public void Execute(Graph graph, Node start)
        {
            VisitedNodes.Clear();
            DFS(graph, start);
        }

        private void DFS(Graph graph, Node node)
        {
            if (VisitedNodes.Contains(node))
                return;

            VisitedNodes.Add(node);

            foreach (var neighbor in graph.GetNeighbors(node))
            {
                DFS(graph, neighbor);
            }
        }
    }
}
