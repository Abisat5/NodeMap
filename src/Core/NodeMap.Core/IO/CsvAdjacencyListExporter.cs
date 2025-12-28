using NodeMap.Core.Models;
using System.IO;
using System.Linq;
using System.Text;

namespace NodeMap.Core.IO
{
    public class CsvAdjacencyListExporter
    {
        public void Export(Graph graph, string filePath)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Node,Neighbors");

            foreach (var node in graph.Nodes)
            {
                var neighbors = graph.Edges
                    .Where(e => e.Source == node || e.Target == node)
                    .Select(e => e.Source == node ? e.Target.Id : e.Source.Id);

                sb.AppendLine($"{node.Id},{string.Join(";", neighbors)}");
            }

            File.WriteAllText(filePath, sb.ToString());
        }
    }
}
