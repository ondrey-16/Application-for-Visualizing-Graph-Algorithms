namespace AVGA.GraphLibrary;

/// <summary>
/// Abstract class for unweighted graphs objects.
/// </summary>
/// <typeparam name="T">Type of edges weights.</typeparam>
abstract public class GraphBase : IGraphMethods
{
    protected DictionaryRepresentation _representation;

    /// <summary>
    /// Constructs a graph reserved for V vertices.
    /// </summary>
    /// <param name="V">Count of vertices.</param>
    public GraphBase(int V)
    {
        _representation = new DictionaryRepresentation(V);
    }
    /// <summary>
    /// Constructs a graph compatible with data read from stream.
    /// </summary>
    /// <param name="s">Graph data stream</param>
    public GraphBase(Stream s)
    {
        _representation = new DictionaryRepresentation(s);
    }

    abstract public int EdgeCount { get; }
    abstract public bool AddEdge(int u, int v);
    abstract public bool RemoveEdge(int u, int v);
    abstract public object Clone();

    public int VertexCount => _representation.VertexCount;
    public int GetInDegree(int v) => _representation.GetInDegree(v);
    public int GetOutDegree(int v) => _representation.GetOutDegree(v);
    public IEnumerable<int> GetNeighbours(int v)  => _representation.GetNeighbours(v);
    public bool HasEdge(int u, int v) => _representation.HasEdge(u, v);
}


/// <summary>
/// Abstract class for weighted graphs objects.
/// </summary>
/// <typeparam name="T">Type of edges weights.</typeparam>
abstract public class GraphBase<T> : IWeightedGraphMethods<T> where T : INumber<T>
{
    protected DictionaryRepresentation<T> _representation;
    protected bool _isDirected;

    /// <summary>
    /// Constructs a weighted graph reserved for V vertices.
    /// </summary>
    /// <param name="V">Count of vertices.</param>
    public GraphBase(int V)
    {
        _representation = new DictionaryRepresentation<T>(V);
    }
    /// <summary>
    /// Constructs a weighted graph compatible with data read from stream.
    /// </summary>
    /// <param name="s">Graph data stream</param>
    public GraphBase(Stream s)
    {
        _representation = new DictionaryRepresentation<T>(s);
    }

    abstract public int EdgeCount { get; }
    abstract public bool AddEdge(int u, int v);
    abstract public bool RemoveEdge(int u, int v);
    abstract public object Clone();

    public int VertexCount => _representation.VertexCount;
    public int GetInDegree(int v) => _representation.GetInDegree(v);
    public int GetOutDegree(int v) => _representation.GetOutDegree(v);
    public IEnumerable<int> GetNeighbours(int v)  => _representation.GetNeighbours(v);
    public bool HasEdge(int u, int v) => _representation.HasEdge(u, v);
    public IEnumerable<(int, T)> GetOutEdges(int v) => _representation.GetOutEdges(v);
    public T GetEdgeWeight(int u, int v) => _representation.GetEdgeWeight(u, v);
    public void SetEdgeWeight(int u, int v, T w) => _representation.SetEdgeWeight(u, v, w);
}