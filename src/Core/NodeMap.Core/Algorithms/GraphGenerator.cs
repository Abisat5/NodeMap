using NodeMap.Core.Models;
using System;
using System.Collections.Generic;
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

            // -------- Nodes --------
            for (int i = 0; i < nodeCount; i++)
            {
                graph.Nodes.Add(new Node
                {
                    Id = i,
                    Name = ((char)('A' + (i % 26))).ToString() + i,
                    X = rand.Next(50, width - 50),
                    Y = rand.Next(50, height - 50)
                });
            }

            // -------- Connectivity (chain) --------
            for (int i = 0; i < nodeCount - 1; i++)
            {
                graph.Edges.Add(new Edge
                {
                    Source = graph.Nodes[i],
                    Target = graph.Nodes[i + 1],
                    Weight = rand.Next(1, 10)
                });
            }

            // -------- Random edges --------
            int extraEdges = edgeCount - (nodeCount - 1);
            for (int i = 0; i < extraEdges; i++)
            {
                var from = graph.Nodes[rand.Next(nodeCount)];
                var to = graph.Nodes[rand.Next(nodeCount)];

                if (from == to) continue;

                if (!graph.Edges.Any(e =>
                    e.Source == from && e.Target == to))
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
