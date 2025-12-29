using NodeMap.Core.Models;
using System.Text.Json;

namespace NodeMap.Core.IO
{
    public class JsonGraphExporter
    {
        public void Export(Graph graph, string filePath)
        {
            if (graph == null) return;

            var dto = new
            {
                nodes = graph.Nodes.Select(n => new
                {
                    id = n.Id,
                    name = n.Name,
                    x = n.X,
                    y = n.Y,
                    colorArgb = n.Color.ToArgb()
                }),
                edges = graph.Edges.Select(e => new
                {
                    source = e.Source.Id,
                    target = e.Target.Id,
                    weight = e.Weight
                })
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(dto, options);
            File.WriteAllText(filePath, json);
        }
    }
}
