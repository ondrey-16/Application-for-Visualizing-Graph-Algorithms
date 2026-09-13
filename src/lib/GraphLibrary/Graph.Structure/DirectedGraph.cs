namespace AVGA.GraphLibrary;

/// <summary>
/// Class representing a directed graph.
/// </summary>
public class DirectedGraph : GraphBase
{
    public DirectedGraph(int V, RepresentationTypeEnum representationType)
        : base(V, representationType, true)
    {}
    
    public DirectedGraph(Stream s, RepresentationTypeEnum representationType)
        : base(s, representationType, true)
    {}

    public override int EdgeCount => _representation.EdgeCount;

    public override bool AddEdge(int u, int v) => _representation.AddEdge(u, v);

    public override bool RemoveEdge(int u, int v) => _representation.RemoveEdge(u, v);

    public override object Clone()
    {
        DirectedGraph cloned = new(this.VertexCount, this._representationType);
        cloned._representation = (GraphRepresentation) this._representation.Clone();

        return cloned;
    }
}


/// <summary>
/// Class representing a directed weighted graph.
/// </summary>
/// <typeparam name="T">Type of edges weights.</typeparam>
public class DirectedGraph<T> : GraphBase<T> where T : INumber<T>
{
    public DirectedGraph(int V, RepresentationTypeEnum representationType)
        : base(V, representationType, true)
    {}
    
    public DirectedGraph(Stream s, RepresentationTypeEnum representationType)
        : base(s, representationType, true)
    {}

    public override int EdgeCount => _representation.EdgeCount;

    public override bool AddEdge(int u, int v) => _representation.AddEdge(u, v);

    public override bool RemoveEdge(int u, int v) => _representation.RemoveEdge(u, v);

    public override object Clone()
    {
        DirectedGraph<T> cloned = new(this.VertexCount, this._representationType);
        cloned._representation = (GraphRepresentation<T>) this._representation.Clone();

        return cloned;
    }
}