using NodeMap.Core.Models;
using System.IO;
using System.Linq;
using System.Text;

namespace NodeMap.Core.IO
{
    public class CsvAdjacencyMatrixExporter
    {
        public void Export(Graph graph, string filePath)
        {
            var sb = new StringBuilder();
            var nodes = graph.Nodes;

            sb.Append(",");
            sb.AppendLine(string.Join(",", nodes.Select(n => n.Id)));

            foreach (var row in nodes)
            {
                sb.Append(row.Id);

                foreach (var col in nodes)
                {
                    bool connected = graph.Edges.Any(e =>
                        (e.Source == row && e.Target == col) ||
                        (e.Source == col && e.Target == row));

                    sb.Append("," + (connected ? "1" : "0"));
                }
                sb.AppendLine();
            }

            File.WriteAllText(filePath, sb.ToString());
        }
    }
}
