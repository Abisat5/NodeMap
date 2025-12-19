using System.Collections.Generic;

namespace NodeMap.Core.Models
{
    public class Node
    {

        public int Id { get; set; }
        public string Name { get; set; }


        public double Aktiflik { get; set; }
        public double Etkilesim { get; set; }
        public int BaglantiSayisi { get; set; }


        public Dictionary<string, object> Metadata { get; set; } = new();

        public override bool Equals(object obj)
        {
            return obj is Node node && node.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Id} - {Name}";
        }
    }
}
