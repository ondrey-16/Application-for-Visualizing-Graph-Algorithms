namespace AVGA.GraphLibrary;

public class DirectedGraph<T> : WeightedGraph<T> where T : INumber<T>
{
    public DirectedGraph(int V, RepresentationTypeEnum representationType)
        : base(V, representationType) {}
    
    public DirectedGraph(Stream s, RepresentationTypeEnum representationType)
        : base(s, representationType) {}

    public override int EdgeCount => _representation.EdgeCount;

    public override bool AddEdge(int u, int v) => _representation.AddEdge(u, v);

    public override bool RemoveEdge(int u, int v) => _representation.RemoveEdge(u, v);
}