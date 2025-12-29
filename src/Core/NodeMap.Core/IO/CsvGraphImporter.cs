using NodeMap.Core.Models;
using System.Drawing;
using System.Text;

namespace NodeMap.Core.IO
{
    public class CsvGraphImporter
    {
        public Graph Import(string csvPath)
        {
            var graph = new Graph();

            var lines = File.ReadAllLines(csvPath, Encoding.UTF8)
                            .Select(l => l.Trim().Trim('\uFEFF'))
                            .Where(l => !string.IsNullOrWhiteSpace(l))
                            .ToList();

            bool readingNodes = false;
            bool readingEdges = false;

            foreach (var line in lines)
            {
                // ===================== SECTION SWITCH =====================
                if (line.Equals("[NODES]", StringComparison.OrdinalIgnoreCase))
                {
                    readingNodes = true;
                    readingEdges = false;
                    continue;
                }

                if (line.Equals("[EDGES]", StringComparison.OrdinalIgnoreCase))
                {
                    readingNodes = false;
                    readingEdges = true;
                    continue;
                }

                // Header satırlarını atla
                if (line.StartsWith("Id,") || line.StartsWith("Source,"))
                    continue;

                var p = line.Contains(';') ? line.Split(';') : line.Split(',');

                // ===================== NODES =====================
                if (readingNodes)
                {
                    if (p.Length < 4)
                        throw new Exception($"Node satırı hatalı → {line}");

                    int id = int.Parse(p[0]);
                    string name = p[1];
                    int x = int.Parse(p[2]);
                    int y = int.Parse(p[3]);

                    Color color = Color.LightBlue;
                    if (p.Length >= 5 && int.TryParse(p[4], out int argb))
                        color = Color.FromArgb(argb);

                    graph.Nodes.Add(new Node
                    {
                        Id = id,
                        Name = name,
                        X = x,
                        Y = y,
                        Color = color
                    });
                }

                // ===================== EDGES =====================
                if (readingEdges)
                {
                    if (p.Length < 3)
                        throw new Exception($"Edge satırı hatalı → {line}");

                    int sourceId = int.Parse(p[0]);
                    int targetId = int.Parse(p[1]);
                    double weight = double.Parse(p[2]);

                    var source = graph.Nodes.FirstOrDefault(n => n.Id == sourceId);
                    var target = graph.Nodes.FirstOrDefault(n => n.Id == targetId);

                    if (source == null || target == null)
                        continue;

                    graph.Edges.Add(new Edge
                    {
                        Source = source,
                        Target = target,
                        Weight = weight
                    });
                }
            }

            return graph;
        }
    }
}
