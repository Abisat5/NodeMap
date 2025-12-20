using NodeMap.Core.Models;
using System;

namespace NodeMap.Core.Algorithms
{
    public static class WeightCalculator
    {
        public static double Calculate(Node i, Node j)
        {
            double aktiflikFark = Math.Abs(i.Aktiflik - j.Aktiflik);
            double etkilesimFark = Math.Abs(i.Etkilesim - j.Etkilesim);
            double baglantiFark = Math.Abs(i.BaglantiSayisi - j.BaglantiSayisi);

            double weight =
                (1.0 / (1 + aktiflikFark)) *
                (2.0 / (2 + etkilesimFark)) *
                (2.0 / (2 + baglantiFark));

            return weight;
        }
    }
}
