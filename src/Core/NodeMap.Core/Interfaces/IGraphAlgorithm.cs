using NodeMap.Core.Models;

namespace NodeMap.Core.Interfaces
{
    public interface IGraphAlgorithm
    {
        void Execute(Graph graph, Node startNode);
    }
}
