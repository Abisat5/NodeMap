using NodeMap.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace NodeMap.Core.Algorithms
{
    public class CentralityCalculator
    {
        public List<(Node Node, int Degree)> CalculateDegreeCentrality(Graph graph)
        {
            var result = new List<(Node, int)>();

            foreach (var node in graph.Nodes)
            {
                int degree = graph.GetNeighbors(node).Count;
                result.Add((node, degree));
            }

            return result;
        }

        public List<(Node Node, int Degree)> GetTop5CentralNodes(Graph graph)
        {
            return CalculateDegreeCentrality(graph)
                   .OrderByDescending(x => x.Degree)
                   .Take(5)
                   .ToList();
        }
    }
}
