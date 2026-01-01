using NodeMap.Core.Models;
using System;
namespace NodeMap.Core.Algorithms
{
    // düğümler arası dinamik ağırlık hesaplaması
    public static class WeightCalculator
    {
        // iki düğümün kenar ağırlığını hesaplar
        public static double Calculate(Node a, Node b)
        {

            // düğümlerin aktiflik farkı
            double aktiflikFark = a.Aktiflik - b.Aktiflik;
            // düğümlerin etkileşim farkı
            double etkilesimFark = a.Etkilesim - b.Etkilesim;
            // düğümlerin bağlantı sayısı farkı
            double baglantiFark = a.BaglantiSayisi - b.BaglantiSayisi;

            // özellik farklarının kareleri toplamı
            double karelerToplami =
                Math.Pow(aktiflikFark, 2)
                + Math.Pow(etkilesimFark, 2)
                + Math.Pow(baglantiFark, 2);
            // fark azaldıkça ağırlığı büyüten normalleştirilmiş değer
            double agirlik = 1.0 / (1.0 + Math.Sqrt(karelerToplami));
            return agirlik;
        }
    }
}
