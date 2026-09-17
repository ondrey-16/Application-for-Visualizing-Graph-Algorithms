namespace AVGA.GraphLibrary;

/// <summary>
/// Class representing a basic graph.
/// </summary>
public class Graph : GraphBase
{
    /// <summary>
    /// Constructs a basic graph compatible with data read from stream.
    /// </summary>
    /// <param name="s">Graph data stream</param>
    public Graph(int V)
        : base(V)
    {}
    /// <summary>
    /// Constructs a basic graph compatible with data read from stream.
    /// </summary>
    /// <param name="s">Graph data stream</param>
    public Graph(Stream s)
        : base(s)
    {
        List<(int, int)> allEdges = new();
        foreach (var u in _representation.Representation.Keys)
        {
            foreach (var v in _representation.Representation[u])
            {
                allEdges.Add((u, v));
            }
        }

        foreach ((int u, int v) in allEdges)
        {
            if (!_representation.AddEdge(v, u))
            {
                throw new DuplicatedEdgeException(v, u);
            }
        }
    }

    public override int EdgeCount => _representation.EdgeCount / 2;

    public override bool AddEdge(int u, int v) 
        => _representation.AddEdge(u, v) && _representation.AddEdge(v, u);

    public override bool RemoveEdge(int u, int v)
        => _representation.RemoveEdge(u, v) && _representation.RemoveEdge(v, u);

    public override object Clone()
    {
        Graph cloned = new(this.VertexCount);
        cloned._representation = (DictionaryRepresentation) this._representation.Clone();

        return cloned;
    }
}


/// <summary>
/// Class representing a weighted basic graph.
/// </summary>
/// <typeparam name="T">Type of edges weights.</typeparam>
public class Graph<T> : GraphBase<T> where T : INumber<T>
{
    /// <summary>
    /// Constructs a basic weighted graph compatible with data read from stream.
    /// </summary>
    /// <param name="s">Graph data stream</param>
    public Graph(int V)
        : base(V)
    {}
    /// <summary>
    /// Constructs a basic weighted graph compatible with data read from stream.
    /// </summary>
    /// <param name="s">Graph data stream</param>
    public Graph(Stream s)
        : base(s)
    {
        List<(int, int, T)> allEdges = new();
        foreach (var u in _representation.Representation.Keys)
        {
            foreach ((int v, T w) in _representation.Representation[u])
            {
                allEdges.Add((u, v, w));
            }
        }

        foreach ((int u, int v, T w) in allEdges)
        {
            if (!_representation.AddEdge(v, u))
            {
                throw new DuplicatedEdgeException(v, u);
            }
            _representation.SetEdgeWeight(v, u, w);
        }
    }

    public override int EdgeCount => _representation.EdgeCount / 2;

    public override bool AddEdge(int u, int v) 
        => _representation.AddEdge(u, v) && _representation.AddEdge(v, u);

    public override bool RemoveEdge(int u, int v)
        => _representation.RemoveEdge(u, v) && _representation.RemoveEdge(v, u);

    public override object Clone()
    {
        Graph<T> cloned = new(this.VertexCount);
        cloned._representation = (DictionaryRepresentation<T>) this._representation.Clone();

        return cloned;
    }
}