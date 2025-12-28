using NodeMap.Core.Models;
using System.Drawing;
using System.IO;
using System.Linq;

namespace NodeMap.Core.IO
{
    public class CsvGraphImporter
    {
        public Graph Import(string nodesCsvPath, string edgesCsvPath)
        {
            var graph = new Graph();

            // ================= NODES =================
            var nodeLines = File.ReadAllLines(nodesCsvPath).Skip(1);

            foreach (var line in nodeLines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var p = line.Split(',');

                if (p.Length < 5) continue;

                graph.Nodes.Add(new Node
                {
                    Id = int.Parse(p[0]),
                    Name = p[1],
                    X = int.Parse(p[2]),
                    Y = int.Parse(p[3]),
                    Color = Color.FromArgb(int.Parse(p[4]))
                });
            }

            // ================= EDGES =================
            var edgeLines = File.ReadAllLines(edgesCsvPath).Skip(1);

            foreach (var line in edgeLines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var p = line.Split(',');

                if (p.Length < 3) continue;

                int sourceId = int.Parse(p[0]);
                int targetId = int.Parse(p[1]);

                var source = graph.Nodes.FirstOrDefault(n => n.Id == sourceId);
                var target = graph.Nodes.FirstOrDefault(n => n.Id == targetId);

                if (source == null || target == null)
                    continue; // 🔥 çökme yok

                graph.Edges.Add(new Edge
                {
                    Source = source,
                    Target = target,
                    Weight = int.Parse(p[2])
                });
            }

            return graph;
        }
    }
}
