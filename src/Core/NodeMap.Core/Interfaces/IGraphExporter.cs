using NodeMap.Core.Models;

namespace NodeMap.Core.IO
{
    public interface IGraphExporter
    {
        void Export(Graph graph, string filePath);
    }
}
