using NodeMap.Core.Models;
using System.Text;

namespace NodeMap.Core.IO
{
    public class CsvAdjacencyMatrixExporter
    {
        public void Export(Graph graph, string filePath)
        {
            if (graph == null || graph.Nodes.Count == 0)
                return;

            var nodes = graph.Nodes.OrderBy(n => n.Id).ToList();
            var sb = new StringBuilder();

            
            sb.Append("Id");
            foreach (var n in nodes)
                sb.Append($",{n.Id}");
            sb.AppendLine();

            foreach (var rowNode in nodes)
            {
                sb.Append(rowNode.Id);

                foreach (var colNode in nodes)
                {
                    var edge = graph.Edges.FirstOrDefault(e =>
                        (e.Source == rowNode && e.Target == colNode) ||
                        (e.Source == colNode && e.Target == rowNode));

                    sb.Append(edge != null ? $",{edge.Weight}" : ",0");
                }

                sb.AppendLine();
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }
    }
}
