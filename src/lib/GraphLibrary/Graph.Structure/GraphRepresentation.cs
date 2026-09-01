namespace AVGA.GraphLibrary;

public abstract class GraphRepresentation<T> : IGraphRepresentation<T>
{
    protected readonly int _V;
    protected int _E;
    protected readonly int[] _inDegrees;
    protected readonly int[] _outDegrees;

    public int VertexCount => _V;
    public int EdgeCount => _E;

    protected GraphRepresentation(int V)
    {
        _V = V;
        _inDegrees = new int[V];
        _outDegrees = new int[V];
        _E = 0;
    }

    abstract public bool AddEdge(int u, int v, T w);
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
}