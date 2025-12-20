using NodeMap.Core.Models;
using NodeMap.Core.Algorithms;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NodeMap.Core.Infrastructure
{
    public class CsvGraphLoader
    {
        public Graph Load(string path)
        {
            var graph = new Graph();
            var lines = File.ReadAllLines(path).Skip(1);

            var nodeDict = new Dictionary<int, Node>();

            foreach (var line in lines)
            {
                var parts = line.Split(',');

                var node = new Node
                {
                    Id = int.Parse(parts[0]),
                    Name = parts[1],
                    Aktiflik = double.Parse(parts[2]),
                    Etkilesim = double.Parse(parts[3]),
                    BaglantiSayisi = int.Parse(parts[4])
                };

                nodeDict[node.Id] = node;
                graph.Nodes.Add(node);
            }

            foreach (var line in lines)
            {
                var parts = line.Split(',');
                int sourceId = int.Parse(parts[0]);

                var neighbors = parts[5].Trim('"').Split(';');

                foreach (var n in neighbors)
                {
                    int targetId = int.Parse(n);

                    if (graph.GetEdge(nodeDict[sourceId], nodeDict[targetId]) != null)
                        continue;

                    var edge = new Edge
                    {
                        Source = nodeDict[sourceId],
                        Target = nodeDict[targetId],
                        Weight = WeightCalculator.Calculate(
                            nodeDict[sourceId],
                            nodeDict[targetId])
                    };

                    graph.Edges.Add(edge);
                }
            }

            return graph;
        }
    }
}
