using NodeMap.Core.Models;
using NodeMap.Core.Interfaces;
using System.Drawing;
using System.IO;
using System.Text;

namespace NodeMap.Core.IO
{
    public class CsvNodeExporter
    {
        public void Export(Graph graph, string filePath)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Id,Name,X,Y,ColorArgb");

            foreach (var n in graph.Nodes)
            {
                sb.AppendLine($"{n.Id},{n.Name},{n.X},{n.Y},{n.Color.ToArgb()}");
            }

            File.WriteAllText(filePath, sb.ToString());
        }
    }
}
