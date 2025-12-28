using NodeMap.Core.Models;

namespace NodeMap.Core.Interfaces
{
    public interface IGraphImporter
    {
        Graph Import(string filePath);
    }
}
