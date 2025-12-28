using NodeMap.Core.Models;
using NodeMap.Core.Interfaces;   // 🔥 EKSİK OLAN BUYDU
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Drawing;

namespace NodeMap.Core.IO
{
    public class JsonGraphImporter : IGraphImporter
    {
        public Graph Import(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var dto = JsonSerializer.Deserialize<GraphDto>(json)!;

            var graph = new Graph();

            // ===================== NODES =====================
            foreach (var n in dto.Nodes)
            {
                graph.Nodes.Add(new Node
                {
                    Id = n.Id,
                    Name = n.Name,
                    X = (int)n.X,
                    Y = (int)n.Y,
                    Color = Color.FromArgb(n.ColorArgb)
                });
            }

            // ===================== EDGES =====================
            foreach (var e in dto.Edges)
            {
                var source = graph.Nodes.FirstOrDefault(n => n.Id == e.SourceId);
                var target = graph.Nodes.FirstOrDefault(n => n.Id == e.TargetId);

                if (source == null || target == null)
                    continue;

                graph.Edges.Add(new Edge
                {
                    Source = source,
                    Target = target,
                    Weight = e.Weight
                });
            }

            return graph;
        }
    }
}
