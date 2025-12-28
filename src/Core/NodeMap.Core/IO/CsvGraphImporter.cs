using NodeMap.Core.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NodeMap.Core.IO
{
    public class CsvGraphImporter : IGraphImporter
    {
        public Graph Import(string filePath)
        {
            var graph = new Graph();
            var lines = File.ReadAllLines(filePath).Skip(1);

            var nodes = new Dictionary<int, Node>();

            foreach (var line in lines)
            {
                var parts = line.Split(',');

                int sourceId = int.Parse(parts[0]);
                int targetId = int.Parse(parts[1]);
                double weight = double.Parse(parts[2]);

                if (!nodes.ContainsKey(sourceId))
                    nodes[sourceId] = new Node { Id = sourceId };

                if (!nodes.ContainsKey(targetId))
                    nodes[targetId] = new Node { Id = targetId };

                graph.Nodes.Add(nodes[sourceId]);
                graph.Nodes.Add(nodes[targetId]);

                graph.Edges.Add(new Edge
                {
                    Source = nodes[sourceId],
                    Target = nodes[targetId],
                    Weight = weight
                });
            }

            graph.Nodes = graph.Nodes.Distinct().ToList();
            return graph;
        }
    }
}
