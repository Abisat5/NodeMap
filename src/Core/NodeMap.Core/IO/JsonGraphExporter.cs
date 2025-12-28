using NodeMap.Core.Models;
using System.IO;
using System.Text.Json;

namespace NodeMap.Core.IO
{
    public class JsonGraphExporter : IGraphExporter
    {
        public void Export(Graph graph, string filePath)
        {
            var json = JsonSerializer.Serialize(graph, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(filePath, json);
        }
    }
}
