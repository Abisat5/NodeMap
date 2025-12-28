using NodeMap.Core.Models;
using NodeMap.Core.Algorithms;
using System.Globalization;
using System.Text;

namespace NodeMap.Core.Infrastructure
{
    public static class CsvExporter
    {
        // =====================================================
        // 🔹 NODE CSV
        // =====================================================
        public static void ExportNodes(Graph graph, string path)
        {
            var sb = new StringBuilder();
            sb.AppendLine("DugumId,Name,Aktiflik,Etkilesim,BaglantiSayisi,Komsular");

            foreach (var node in graph.Nodes)
            {
                var neighbors = graph.GetNeighbors(node)
                                     .Select(n => n.Id.ToString());

                sb.AppendLine(
                    $"{node.Id}," +
                    $"{node.Name}," +
                    $"{node.Aktiflik.ToString(CultureInfo.InvariantCulture)}," +
                    $"{node.Etkilesim.ToString(CultureInfo.InvariantCulture)}," +
                    $"{node.BaglantiSayisi}," +
                    $"\"{string.Join(";", neighbors)}\""
                );
            }

            File.WriteAllText(path, sb.ToString());
        }

        // =====================================================
        // 🔹 EDGE CSV (DİNAMİK AĞIRLIKLI)
        // =====================================================
        public static void ExportEdges(Graph graph, string path)
        {
            var sb = new StringBuilder();
            sb.AppendLine("SourceId,TargetId,Weight");

            foreach (var edge in graph.Edges)
            {
                sb.AppendLine(
                    $"{edge.Source.Id}," +
                    $"{edge.Target.Id}," +
                    $"{edge.Weight.ToString("F4", CultureInfo.InvariantCulture)}"
                );
            }

            File.WriteAllText(path, sb.ToString());
        }

        // =====================================================
        // 🔹 CENTRALITY CSV
        // =====================================================
        public static void ExportCentrality(
            string algorithmName,
            IEnumerable<(Node node, double value)> results,
            string path)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Algorithm,NodeId,NodeName,Value");

            foreach (var (node, value) in results)
            {
                sb.AppendLine(
                    $"{algorithmName}," +
                    $"{node.Id}," +
                    $"{node.Name}," +
                    $"{value.ToString("F4", CultureInfo.InvariantCulture)}"
                );
            }

            File.WriteAllText(path, sb.ToString());
        }
    }
}
