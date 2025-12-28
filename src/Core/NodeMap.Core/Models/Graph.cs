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

        // ======================================================
        // 🔁 DEEP CLONE (UNDO / BAŞLANGIÇ GRAFA DÖN)
        // ======================================================
        public Graph Clone()
        {
            var cloneGraph = new Graph();

            // 1️⃣ Node'ları kopyala
            var nodeMap = new Dictionary<Node, Node>();

            foreach (var node in Nodes)
            {
                var newNode = new Node
                {
                    Id = node.Id,
                    Name = node.Name,
                    X = node.X,
                    Y = node.Y,
                    Aktiflik = node.Aktiflik,
                    Etkilesim = node.Etkilesim,
                    BaglantiSayisi = node.BaglantiSayisi,
                    Color = node.Color
                };

                nodeMap[node] = newNode;
                cloneGraph.Nodes.Add(newNode);
            }

            // 2️⃣ Edge'leri kopyala
            foreach (var edge in Edges)
            {
                var newEdge = new Edge
                {
                    Source = nodeMap[edge.Source],
                    Target = nodeMap[edge.Target],
                    Weight = edge.Weight
                };

                cloneGraph.Edges.Add(newEdge);
            }

            return cloneGraph;
        }
    }
}
