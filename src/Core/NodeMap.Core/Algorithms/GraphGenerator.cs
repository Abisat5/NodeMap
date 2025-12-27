using NodeMap.Core.Models;
using System;
using System.Linq;

namespace NodeMap.Core.Algorithms
{
    public static class GraphGenerator
    {
        public static Graph GenerateRandomGraph(
            int nodeCount,
            int edgeCount,
            int width,
            int height)
        {
            var rand = new Random();
            var graph = new Graph();

            // ================= NODE YERLEŞİMİ (GRID) =================
            int cols = (int)Math.Ceiling(Math.Sqrt(nodeCount));
            int rows = (int)Math.Ceiling((double)nodeCount / cols);

            int padding = 80;
            int cellWidth = (width - padding * 2) / cols;
            int cellHeight = (height - padding * 2) / rows;

            for (int i = 0; i < nodeCount; i++)
            {
                int row = i / cols;
                int col = i % cols;

                int x = padding + col * cellWidth + rand.Next(-15, 15);
                int y = padding + row * cellHeight + rand.Next(-15, 15);

                graph.Nodes.Add(new Node
                {
                    Id = i,
                    Name = ((char)('A' + (i % 26))).ToString() + i,
                    X = x,
                    Y = y
                });
            }

            // ================= ZORUNLU BAĞLANTI (CHAIN) =================
            for (int i = 0; i < nodeCount - 1; i++)
            {
                graph.Edges.Add(new Edge
                {
                    Source = graph.Nodes[i],
                    Target = graph.Nodes[i + 1],
                    Weight = rand.Next(1, 10)
                });
            }

            // ================= EK RANDOM EDGE =================
            int extraEdges = edgeCount - (nodeCount - 1);
            for (int i = 0; i < extraEdges; i++)
            {
                var from = graph.Nodes[rand.Next(nodeCount)];
                var to = graph.Nodes[rand.Next(nodeCount)];

                if (from == to) continue;

                if (!graph.Edges.Any(e =>
                    (e.Source == from && e.Target == to) ||
                    (e.Source == to && e.Target == from)))
                {
                    graph.Edges.Add(new Edge
                    {
                        Source = from,
                        Target = to,
                        Weight = rand.Next(1, 10)
                    });
                }
            }

            return graph;
        }
    }
}
