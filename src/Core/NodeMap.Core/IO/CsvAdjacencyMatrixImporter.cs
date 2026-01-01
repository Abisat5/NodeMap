using NodeMap.Core.Models;
using System.Text;

namespace NodeMap.Core.IO
{
    public class CsvAdjacencyMatrixImporter
    {
        public Graph Import(string path)
        {
            var graph = new Graph();
            var lines = File.ReadAllLines(path, Encoding.UTF8);

            
            var headers = lines[0].Split(',').Skip(1).Select(int.Parse).ToList();

            foreach (var id in headers)
            {
                graph.Nodes.Add(new Node
                {
                    Id = id,
                    Name = $"N{id}",
                    X = 0,
                    Y = 0
                });
            }

            for (int i = 1; i < lines.Length; i++)
            {
                var row = lines[i].Split(',');
                int sourceId = int.Parse(row[0]);
                var source = graph.Nodes.First(n => n.Id == sourceId);

                for (int j = 1; j < row.Length; j++)
                {
                    int weight = int.Parse(row[j]);
                    if (weight <= 0) continue;

                    int targetId = headers[j - 1];
                    var target = graph.Nodes.First(n => n.Id == targetId);

                    if (!graph.Edges.Any(e =>
                        (e.Source == source && e.Target == target) ||
                        (e.Source == target && e.Target == source)))
                    {
                        graph.Edges.Add(new Edge
                        {
                            Source = source,
                            Target = target,
                            Weight = weight
                        });
                    }
                }
            }

            return graph;
        }
    }
}
