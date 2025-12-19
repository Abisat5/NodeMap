namespace NodeMap.Core.Models
{
    public class Node
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public double Activity { get; set; }
        public double Interaction { get; set; }

        public int ConnectionCount { get; set; }
    }
}
