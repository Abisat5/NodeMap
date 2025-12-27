using NodeMap.Core.Models;

namespace NodeMap.Core.Interfaces
{
    public interface IGraphAlgorithm
    {
        long ElapsedMilliseconds { get; }
        void Execute(Graph graph, Node start);
    }
}
