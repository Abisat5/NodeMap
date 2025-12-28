namespace NodeMap.Core.IO
{
    public class GraphDto
    {
        public List<NodeDto> Nodes { get; set; } = new();
        public List<EdgeDto> Edges { get; set; } = new();
    }

    public class NodeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public double X { get; set; }
        public double Y { get; set; }
        public int ColorArgb { get; set; }
    }


    public class EdgeDto
    {
        public int SourceId { get; set; }
        public int TargetId { get; set; }
        public double Weight { get; set; } 
    }

}
