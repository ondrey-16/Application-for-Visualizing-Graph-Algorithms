namespace AVGA.GraphLibrary;

public class UndirectedGraph<T> : WeightedGraph<T> where T : INumber<T>
{
    public UndirectedGraph(int V, RepresentationTypeEnum representationType)
        : base(V, representationType, false)
    {}
    
    public UndirectedGraph(Stream s, RepresentationTypeEnum representationType)
        : base(s, representationType, false)
    {}

    public override int EdgeCount => _representation.EdgeCount / 2;

    public override bool AddEdge(int u, int v) 
        => _representation.AddEdge(u, v) && _representation.AddEdge(v, u);

    public override bool RemoveEdge(int u, int v)
        => _representation.RemoveEdge(u, v) && _representation.RemoveEdge(v, u);

    public override object Clone()
    {
        UndirectedGraph<T> cloned = new(this.VertexCount, this._representationType);
        cloned._representation = (GraphRepresentation<T>) this._representation.Clone();

        return cloned;
    }
}