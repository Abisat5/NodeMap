using NodeMap.Core.Models;
using System;

namespace NodeMap.Core.Algorithms
{
    public static class WeightCalculator
    {
        public static double Calculate(Node a, Node b)
        {
            double aktiflikDiff = Math.Abs(a.Aktiflik - b.Aktiflik);
            double etkilesimDiff = Math.Abs(a.Etkilesim - b.Etkilesim);
            double baglantiDiff = Math.Abs(a.BaglantiSayisi - b.BaglantiSayisi);

            return
                (1.0 / (1.0 + aktiflikDiff)) *
                (2.0 / (2.0 + etkilesimDiff)) *
                (2.0 / (2.0 + baglantiDiff));
        }
    }
}
