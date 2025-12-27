using NodeMap.Core.Interfaces;
using NodeMap.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NodeMap.Core.Algorithms
{
    public class AStarAlgorithm : IGraphAlgorithm
    {
        private Dictionary<Node, Node> _cameFrom = new();
        private Dictionary<Node, double> _gScore = new();
        private Dictionary<Node, double> _fScore = new();

        public long ElapsedMilliseconds { get; private set; }

        public void Execute(Graph graph, Node start)
        {
            // A* için Execute boþ kalabilir
        }

        public List<Node> FindPath(Graph graph, Node start, Node goal)
        {
            var sw = Stopwatch.StartNew();

            var openSet = new List<Node> { start };

            _cameFrom.Clear();
            _gScore.Clear();
            _fScore.Clear();

            foreach (var node in graph.Nodes)
            {
                _gScore[node] = double.MaxValue;
                _fScore[node] = double.MaxValue;
            }

            _gScore[start] = 0;
            _fScore[start] = Heuristic(start, goal);

            while (openSet.Any())
            {
                var current = openSet.OrderBy(n => _fScore[n]).First();

                if (current == goal)
                {
                    sw.Stop();
                    ElapsedMilliseconds = sw.ElapsedMilliseconds;
                    return ReconstructPath(current);
                }

                openSet.Remove(current);

                foreach (var neighbor in graph.GetNeighbors(current))
                {
                    var edge = graph.GetEdge(current, neighbor);
                    if (edge == null) continue;

                    double tentativeG = _gScore[current] + edge.Weight;

                    if (tentativeG < _gScore[neighbor])
                    {
                        _cameFrom[neighbor] = current;
                        _gScore[neighbor] = tentativeG;
                        _fScore[neighbor] =
                            tentativeG + Heuristic(neighbor, goal);

                        if (!openSet.Contains(neighbor))
                            openSet.Add(neighbor);
                    }
                }
            }

            sw.Stop();
            ElapsedMilliseconds = sw.ElapsedMilliseconds;
            return new List<Node>();
        }

        private double Heuristic(Node a, Node b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        private List<Node> ReconstructPath(Node current)
        {
            var path = new List<Node> { current };
            while (_cameFrom.ContainsKey(current))
            {
                current = _cameFrom[current];
                path.Add(current);
            }
            path.Reverse();
            return path;
        }
    }
}
