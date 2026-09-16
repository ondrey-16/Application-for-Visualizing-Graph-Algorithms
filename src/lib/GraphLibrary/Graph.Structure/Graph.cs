namespace AVGA.GraphLibrary;

/// <summary>
/// Class representing a basic graph.
/// </summary>
public class Graph : GraphBase
{
    public Graph(int V, RepresentationTypeEnum representationType)
        : base(V, representationType, false)
    {}
    
    public Graph(Stream s, RepresentationTypeEnum representationType)
        : base(s, representationType, false)
    {}

    public override int EdgeCount => _representation.EdgeCount / 2;

    public override bool AddEdge(int u, int v) 
        => _representation.AddEdge(u, v) && _representation.AddEdge(v, u);

    public override bool RemoveEdge(int u, int v)
        => _representation.RemoveEdge(u, v) && _representation.RemoveEdge(v, u);

    public override object Clone()
    {
        Graph cloned = new(this.VertexCount, this._representationType);
        cloned._representation = (GraphRepresentation) this._representation.Clone();

        return cloned;
    }
}


/// <summary>
/// Class representing a weighted basic graph.
/// </summary>
/// <typeparam name="T">Type of edges weights.</typeparam>
public class Graph<T> : GraphBase<T> where T : INumber<T>
{
    public Graph(int V, RepresentationTypeEnum representationType)
        : base(V, representationType, false)
    {}
    
    public Graph(Stream s, RepresentationTypeEnum representationType)
        : base(s, representationType, false)
    {}

    public override int EdgeCount => _representation.EdgeCount / 2;

    public override bool AddEdge(int u, int v) 
        => _representation.AddEdge(u, v) && _representation.AddEdge(v, u);

    public override bool RemoveEdge(int u, int v)
        => _representation.RemoveEdge(u, v) && _representation.RemoveEdge(v, u);

    public override object Clone()
    {
        Graph<T> cloned = new(this.VertexCount, this._representationType);
        cloned._representation = (GraphRepresentation<T>) this._representation.Clone();

        return cloned;
    }
}