using NodeMap.Core.Models;
using System.Text;

namespace NodeMap.Core.IO
{
    public class CsvAdjacencyListExporter
    {
        public void Export(Graph graph, string filePath)
        {
            if (graph == null || graph.Nodes.Count == 0)
                return;

            var sb = new StringBuilder();

            
            sb.AppendLine("NodeId,Neighbors");

            foreach (var node in graph.Nodes.OrderBy(n => n.Id))
            {
                var neighbors = graph.Edges
                    .Where(e => e.Source == node || e.Target == node)
                    .Select(e => e.Source == node ? e.Target.Id : e.Source.Id)
                    .Distinct()
                    .OrderBy(id => id);

                sb.AppendLine($"{node.Id},{string.Join(";", neighbors)}");
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }
    }
}
