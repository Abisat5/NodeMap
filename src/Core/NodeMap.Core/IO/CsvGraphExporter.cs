using NodeMap.Core.Models;
using System.Text;

namespace NodeMap.Core.IO
{
    public class CsvGraphExporter
    {
        public void Export(Graph graph, string path)
        {
            var sb = new StringBuilder();

            sb.AppendLine("[NODES]");
            sb.AppendLine("Id,Name,X,Y,ColorArgb");
            foreach (var n in graph.Nodes)
            {
                sb.AppendLine($"{n.Id},{n.Name},{n.X},{n.Y},{n.Color.ToArgb()}");
            }

            sb.AppendLine();
            sb.AppendLine("[EDGES]");
            sb.AppendLine("Source,Target,Weight");
            foreach (var e in graph.Edges)
            {
                sb.AppendLine($"{e.Source.Id},{e.Target.Id},{e.Weight}");
            }

            File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
        }
    }
}
