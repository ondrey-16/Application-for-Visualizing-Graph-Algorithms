namespace AVGA.GraphLibrary;

/// <summary>
/// Class representing a directed graph.
/// </summary>
public class DirectedGraph : GraphBase
{
    /// <summary>
    /// Constructs an empty digraph reserved for V vertices.
    /// </summary>
    /// <param name="V">Count of vertices</param>
    public DirectedGraph(int V)
        : base(V)
    {}
    /// <summary>
    /// Constructs a digraph compatible with data read from stream.
    /// </summary>
    /// <param name="s">Graph data stream</param>
    public DirectedGraph(Stream s)
        : base(s)
    {}

    public override int EdgeCount => _representation.EdgeCount;
    public override bool AddEdge(int u, int v) => _representation.AddEdge(u, v);
    public override bool RemoveEdge(int u, int v) => _representation.RemoveEdge(u, v);
    public override object Clone()
    {
        DirectedGraph cloned = new(this.VertexCount);
        cloned._representation = (DictionaryRepresentation) this._representation.Clone();

        return cloned;
    }
}


/// <summary>
/// Class representing a directed weighted graph.
/// </summary>
/// <typeparam name="T">Type of edges weights.</typeparam>
public class DirectedGraph<T> : GraphBase<T> where T : INumber<T>, IMinMaxValue<T>
{
    /// <summary>
    /// Constructs an empty weighted digraph reserved for V vertices.
    /// </summary>
    /// <param name="V">Count of vertices</param>
    public DirectedGraph(int V)
        : base(V)
    {}
    /// <summary>
    /// Constructs a weighted digraph compatible with data read from stream.
    /// </summary>
    /// <param name="s">Graph data stream</param>
    public DirectedGraph(Stream s)
        : base(s)
    {}

    public override int EdgeCount => _representation.EdgeCount;
    public override bool AddEdge(int u, int v) => _representation.AddEdge(u, v);
    public override bool RemoveEdge(int u, int v) => _representation.RemoveEdge(u, v);
    public override object Clone()
    {
        DirectedGraph<T> cloned = new(this.VertexCount);
        cloned._representation = (DictionaryRepresentation<T>) this._representation.Clone();

        return cloned;
    }
}