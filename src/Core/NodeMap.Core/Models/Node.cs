namespace NodeMap.Core.Models
{
    public class Node
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int X { get; set; }
        public int Y { get; set; }

        public double Aktiflik { get; set; }
        public double Etkilesim { get; set; }
        public int BaglantiSayisi { get; set; }
    }
}
