namespace NodeMap.Core.Models
{
    public class Edge
    {
        public Node Source { get; set; }
        public Node Target { get; set; }
        public double Weight { get; set; }
    }
}
