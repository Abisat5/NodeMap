using NodeMap.Core.Models;
using System.Text;

namespace NodeMap.Core.IO
{
    public class CsvAdjacencyListImporter
    {
        public Graph Import(string path)
        {
            var graph = new Graph();
            var lines = File.ReadAllLines(path, Encoding.UTF8);

            // HEADER: NodeId,Neighbors
            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var p = line.Split(',');
                int nodeId = int.Parse(p[0]);

                var node = graph.Nodes.FirstOrDefault(n => n.Id == nodeId);
                if (node == null)
                {
                    node = new Node
                    {
                        Id = nodeId,
                        Name = $"N{nodeId}",
                        X = 0,
                        Y = 0
                    };
                    graph.Nodes.Add(node);
                }

                if (p.Length < 2 || string.IsNullOrWhiteSpace(p[1]))
                    continue;

                var neighbors = p[1].Split(';');
                foreach (var n in neighbors)
                {
                    int targetId = int.Parse(n);

                    var target = graph.Nodes.FirstOrDefault(x => x.Id == targetId);
                    if (target == null)
                    {
                        target = new Node
                        {
                            Id = targetId,
                            Name = $"N{targetId}",
                            X = 0,
                            Y = 0
                        };
                        graph.Nodes.Add(target);
                    }

                    if (!graph.Edges.Any(e =>
                        (e.Source == node && e.Target == target) ||
                        (e.Source == target && e.Target == node)))
                    {
                        graph.Edges.Add(new Edge
                        {
                            Source = node,
                            Target = target,
                            Weight = 1
                        });
                    }
                }
            }

            return graph;
        }
    }
}
