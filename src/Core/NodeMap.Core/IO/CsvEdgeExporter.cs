using NodeMap.Core.Models;
using NodeMap.Core.Interfaces;
using System.IO;
using System.Text;

namespace NodeMap.Core.IO
{
    public class CsvEdgeExporter : IGraphExporter
    {
        public void Export(Graph graph, string filePath)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Source,Target,Weight");

            foreach (var e in graph.Edges)
            {
                sb.AppendLine($"{e.Source.Id},{e.Target.Id},{e.Weight}");
            }

            File.WriteAllText(filePath, sb.ToString());
        }
    }
}
