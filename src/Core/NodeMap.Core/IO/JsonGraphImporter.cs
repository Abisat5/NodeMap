using NodeMap.Core.Models;
using System.IO;
using System.Text.Json;

namespace NodeMap.Core.IO
{
    public class JsonGraphImporter : IGraphImporter
    {
        public Graph Import(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Graph>(json)!;
        }
    }
}
