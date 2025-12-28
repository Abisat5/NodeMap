using NodeMap.Core.Models;

namespace NodeMap.Core.IO
{
    public interface IGraphImporter
    {
        Graph Import(string filePath);
    }
}
