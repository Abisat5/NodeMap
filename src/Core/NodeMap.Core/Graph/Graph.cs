using System.Collections.Generic;
using System.Linq;

namespace NodeMap.Core.Models
{
    public class Graph
    {
        public List<Node> Nodes { get; set; } = new();
        public List<Edge> Edges { get; set; } = new();

        public List<Node> GetNeighbors(Node node)
        {
            return Edges
                .Where(e => e.Source.Equals(node))
                .Select(e => e.Target)
                .Concat(
                    Edges.Where(e => e.Target.Equals(node))
                         .Select(e => e.Source)
                )
                .Distinct()
                .ToList();
        }

        public Edge? GetEdge(Node a, Node b)
        {
            return Edges.FirstOrDefault(e =>
                (e.Source.Equals(a) && e.Target.Equals(b)) ||
                (e.Source.Equals(b) && e.Target.Equals(a)));
        }

        public List<(Node node, double weight)> GetWeightedNeighbors(Node node)
        {
            return Edges
                .Where(e => e.Source == node || e.Target == node)
                .Select(e =>
                {
                    var neighbor = e.Source == node ? e.Target : e.Source;
                    return (neighbor, e.Weight);
                })
                .ToList();
        }

    }

}
