using NodeMap.Core.Models;

namespace NodeMap.Core.Utils
{
    public static class GraphSnapshot
    {
        private static Graph? _initial;

        public static void Save(Graph graph)
        {
            _initial = graph.Clone();
        }

        public static Graph Restore()
        {
            return _initial!.Clone();
        }
    }
}
