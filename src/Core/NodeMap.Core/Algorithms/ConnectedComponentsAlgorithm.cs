using NodeMap.Core.Models;
using System.Collections.Generic;

namespace NodeMap.Core.Algorithms
{
    // grafýn baðlý bileþenlerini bulur
    public class ConnectedComponentsAlgorithm
    {
        // graf içindeki tüm baðlý bileþenleri liste olarak döndürür
        public List<List<Node>> Find(Graph graph)
        {
            // ziyaret edilen düðümleri tutar
            var visited = new HashSet<Node>();

            // bulunan tüm bileþenleri saklar
            var components = new List<List<Node>>();

            // graf üzerindeki her düðüm için dolaþým
            foreach (var node in graph.Nodes)
            {
                // daha önce ziyaret edildiyse atlanýr
                if (visited.Contains(node)) continue;

                // yeni bir baðlý bileþen oluþturulur
                var component = new List<Node>();

                // dfs ile bu bileþene ait düðümler toplanýr
                DFS(graph, node, visited, component);

                // bulunan bileþen listeye eklenir
                components.Add(component);
            }

            // tüm baðlý bileþenler döndürülür
            return components;
        }

        // derinlik öncelikli arama 
        private void DFS(Graph graph, Node node,
                         HashSet<Node> visited,
                         List<Node> component)
        {
            
            visited.Add(node);

            // düðüm mevcut bileþene eklenir
            component.Add(node);

            // komþu düðümler üzerinde dfs devam eder
            foreach (var n in graph.GetNeighbors(node))
            {
                if (!visited.Contains(n))
                    DFS(graph, n, visited, component);
            }
        }
    }
}
