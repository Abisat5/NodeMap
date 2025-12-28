using NodeMap.Core.Models;
using System.IO;
using System.Text.Json;

namespace NodeMap.Core.IO
{
    public class JsonGraphExporter : IGraphExporter
    {
        public void Export(Graph graph, string filePath)
        {
            var dto = new GraphDto
            {
                Nodes = graph.Nodes.Select(n => new NodeDto
                {
                    Id = n.Id,
                    Name = n.Name,
                    X = (double)n.X,   // 🔥 AÇIK CAST
                    Y = (double)n.Y,   // 🔥 AÇIK CAST
                    ColorArgb = n.Color.ToArgb()
                }).ToList(),

                Edges = graph.Edges.Select(e => new EdgeDto
                {
                    SourceId = e.Source.Id,
                    TargetId = e.Target.Id,
                    Weight = e.Weight
                }).ToList()
            };

            var json = JsonSerializer.Serialize(dto, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(filePath, json);
        }
    }
}
