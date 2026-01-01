using NodeMap.Core.Interfaces;
using NodeMap.Core.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace NodeMap.Core.Algorithms
{
    // derinlik öncelikli arama algoritmasý
    public class DFSAlgorithm : IGraphAlgorithm
    {
        // ziyaret edilen düðümlerin sýrasýný tutar
        public List<Node> VisitedNodes { get; private set; } = new();

        // algoritmanýn çalýþma süresini milisaniye cinsinden saklar
        public long ElapsedMilliseconds { get; private set; }

        // dfs algoritmasýný verilen baþlangýç düðümünden çalýþtýrýr
        public void Execute(Graph graph, Node start)
        {
            // süre ölçümü baþlatýlýr
            var sw = Stopwatch.StartNew();

            // önceki çalýþmadan kalan düðümler temizlenir
            VisitedNodes.Clear();

            // recursive dfs çaðrýsý baþlatýlýr
            DFS(graph, start);

            // süre ölçümü durdurulur
            sw.Stop();

            // geçen süre kaydedilir
            ElapsedMilliseconds = sw.ElapsedMilliseconds;
        }

        // recursive olarak graf üzerinde gezinir
        private void DFS(Graph graph, Node node)
        {
            // düðüm daha önce ziyaret edildiyse geri dönülür
            if (VisitedNodes.Contains(node))
                return;

            // düðüm ziyaret edildi olarak iþaretlenir
            VisitedNodes.Add(node);

            // komþu düðümler üzerinde dfs devam ettirilir
            foreach (var neighbor in graph.GetNeighbors(node))
            {
                DFS(graph, neighbor);
            }
        }
    }
}
