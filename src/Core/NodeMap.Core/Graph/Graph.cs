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
                .Where(e => e.Source == node || e.Target == node)
                .Select(e => e.Source == node ? e.Target : e.Source)
                .ToList();
        }
    }
}
