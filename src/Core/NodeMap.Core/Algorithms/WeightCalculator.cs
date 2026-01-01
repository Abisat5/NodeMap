using NodeMap.Core.Models;
using System;

namespace NodeMap.Core.Algorithms
{
    public static class WeightCalculator
    {
        public static double Calculate(Node a, Node b)
        {
            // Özellik farklarını hesapla
            double aktiflikFark = a.Aktiflik - b.Aktiflik;
            double etkilesimFark = a.Etkilesim - b.Etkilesim;
            double baglantiFark = a.BaglantiSayisi - b.BaglantiSayisi;

            // Formüle göre ağırlık hesapla:
            // Ağırlık_ij = 1 / (1 + sqrt((Aktiflik_i - Aktiflik_j)^2 + (Etkilesim_i - Etkilesim_j)^2 + (Bağlantı_i - Bağlantı_j)^2))
            double karelerToplami =
                Math.Pow(aktiflikFark, 2)
                + Math.Pow(etkilesimFark, 2)
                + Math.Pow(baglantiFark, 2);

            double agirlik = 1.0 / (1.0 + Math.Sqrt(karelerToplami));
            return agirlik;
        }
    }
}