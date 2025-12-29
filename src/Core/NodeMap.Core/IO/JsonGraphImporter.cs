using NodeMap.Core.Models;
using System.Drawing;
using System.Text.Json;

namespace NodeMap.Core.IO
{
    public class JsonGraphImporter
    {
        private class NodeDto
        {
            public int id { get; set; }
            public string name { get; set; } = "";
            public int x { get; set; }
            public int y { get; set; }
            public int colorArgb { get; set; }
        }

        private class EdgeDto
        {
            public int source { get; set; }
            public int target { get; set; }
            public double weight { get; set; }
        }

        private class GraphDto
        {
            public List<NodeDto> nodes { get; set; } = new();
            public List<EdgeDto> edges { get; set; } = new();
        }

        public Graph Import(string filePath)
        {
            string json = File.ReadAllText(filePath);
            var dto = JsonSerializer.Deserialize<GraphDto>(json);

            if (dto == null) throw new Exception("JSON okunamadı");

            var graph = new Graph();

            // NODES
            foreach (var n in dto.nodes)
            {
                graph.Nodes.Add(new Node
                {
                    Id = n.id,
                    Name = n.name,
                    X = n.x,
                    Y = n.y,
                    Color = Color.FromArgb(n.colorArgb)
                });
            }

            // EDGES
            foreach (var e in dto.edges)
            {
                var source = graph.Nodes.FirstOrDefault(n => n.Id == e.source);
                var target = graph.Nodes.FirstOrDefault(n => n.Id == e.target);
                if (source == null || target == null) continue;

                graph.Edges.Add(new Edge
                {
                    Source = source,
                    Target = target,
                    Weight = e.weight
                });
            }

            return graph;
        }
    }
}
