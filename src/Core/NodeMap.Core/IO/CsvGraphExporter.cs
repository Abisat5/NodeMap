using NodeMap.Core.Models;
using System.IO;
using System.Text;

namespace NodeMap.Core.IO
{
    public class CsvGraphExporter : IGraphExporter
    {
        public void Export(Graph graph, string filePath)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Source,Target,Weight");

            foreach (var edge in graph.Edges)
            {
                sb.AppendLine($"{edge.Source.Id},{edge.Target.Id},{edge.Weight}");
            }

            File.WriteAllText(filePath, sb.ToString());
        }
    }
}
