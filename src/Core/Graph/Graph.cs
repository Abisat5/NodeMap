using System.Collections.Generic;

namespace NodeMap.Core.Models
{
    public class Graph
    {
        public List<Node> Nodes { get; } = new();
        public List<Edge> Edges { get; } = new();

        public void AddNode(Node node)
        {
            // TODO: prevent duplicate nodes
        }

        public void AddEdge(Node source, Node target)
        {
            // TODO: prevent self-loop & duplicates
        }
    }
}
