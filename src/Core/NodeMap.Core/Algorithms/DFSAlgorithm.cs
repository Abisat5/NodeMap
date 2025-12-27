using NodeMap.Core.Interfaces;
using NodeMap.Core.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace NodeMap.Core.Algorithms
{
    public class DFSAlgorithm : IGraphAlgorithm
    {
        public List<Node> VisitedNodes { get; private set; } = new();
        public long ElapsedMilliseconds { get; private set; }

        public void Execute(Graph graph, Node start)
        {
            var sw = Stopwatch.StartNew();

            VisitedNodes.Clear();
            DFS(graph, start);

            sw.Stop();
            ElapsedMilliseconds = sw.ElapsedMilliseconds;
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
