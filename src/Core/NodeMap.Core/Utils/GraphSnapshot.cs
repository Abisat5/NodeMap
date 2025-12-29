using NodeMap.Core.Models;

namespace NodeMap.Core.Utils
{
    public static class GraphSnapshot
    {
        private static Graph? _initial;

        // 🔹 İlk graph'ı kaydet
        public static void Save(Graph graph)
        {
            _initial = graph.Clone();
        }

        // 🔹 Kaydedilen ilk graph'a dön
        public static Graph Restore()
        {
            if (_initial == null)
                throw new InvalidOperationException("Henüz başlangıç graph'ı kaydedilmedi.");

            return _initial.Clone();
        }

        // (Opsiyonel) resetlemek istersen
        public static void Clear()
        {
            _initial = null;
        }
    }
}
