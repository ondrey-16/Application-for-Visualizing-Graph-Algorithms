namespace AVGA.GraphLibrary;

public abstract class GraphRepresentation<T> : IGraphMethods<T>, IWeightedGraphMethods<T> where T : INumber<T>
{
    protected int _V;
    protected int _E;
    protected readonly List<int> _inDegrees;
    protected readonly List<int> _outDegrees;

    public int VertexCount => _V;
    public int EdgeCount => _E;

    public GraphRepresentation()
    {
        _inDegrees = new();
        _outDegrees = new();
    }
    public GraphRepresentation(int V)
    {
        _V = V;
        _inDegrees = Enumerable.Repeat(0, V).ToList();
        _outDegrees = Enumerable.Repeat(0, V).ToList();
        _E = 0;
    }

    abstract public bool AddEdge(int u, int v);
    abstract public bool RemoveEdge(int u, int v);
    abstract public T GetEdgeWeight(int u, int v);
    abstract public IEnumerable<int> GetNeighbours(int v);
    abstract public IEnumerable<(int, T)> GetOutEdges(int v);
    abstract public void SetEdgeWeight(int u, int v, T w);
    abstract public bool HasEdge(int u, int v);

    public int GetInDegree(int v)
    {
        CheckVertex(v);

        return _inDegrees[v];
    }
    public int GetOutDegree(int v)
    {
        CheckVertex(v);

        return _outDegrees[v];
    }

    /// <summary>
    /// Checks if vertex is valid.
    /// </summary>
    /// <param name="v">Vertex</param>
    /// <exception cref="InvalidVertexException">If vertex is out of range.</exception>
    protected void CheckVertex(int v)
    {
        if (v < 0 || v >= _V)
        {
            throw new InvalidVertexException(v, _V);
        }
    }

    /// <summary>
    /// Checks if edge is valid.
    /// </summary>
    /// <param name="u">Start of the edge.</param>
    /// <param name="v">End of the edge.</param>
    /// <exception cref="InvalidEdgeException">If at least one of the vertices is out of range.</exception>
    protected void CheckEdge(int u, int v)
    {
        if (u < 0 || u >= _V || v < 0 || v >= _V)
        {
            throw new InvalidEdgeException(u, v, _V);
        }
    }

    public abstract object Clone();
}