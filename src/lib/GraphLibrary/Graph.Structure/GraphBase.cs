namespace AVGA.GraphLibrary;

/// <summary>
/// Abstract class for unweighted graphs objects.
/// </summary>
/// <typeparam name="T">Type of edges weights.</typeparam>
abstract public class GraphBase : IGraphMethods
{
    protected GraphRepresentation _representation;
    protected RepresentationTypeEnum _representationType;
    protected readonly bool _isDirected;
    public GraphBase(int V, RepresentationTypeEnum representationType, bool isDirected)
    {
        _isDirected = isDirected;
        _representationType = representationType;
        _representation = representationType switch
        {
            RepresentationTypeEnum.LIST => new ListGraphRepresentation(V),
            RepresentationTypeEnum.MATRIX => new MatrixGraphRepresentation(V),
            _ => throw new InvalidRepresentationTypeException()
        };
    }
    public GraphBase(Stream s, RepresentationTypeEnum representationType, bool isDirected)
    {
        _isDirected = isDirected;
        _representationType = representationType;
        _representation = representationType switch
        {
            RepresentationTypeEnum.LIST => new ListGraphRepresentation(s, _isDirected),
            RepresentationTypeEnum.MATRIX => new MatrixGraphRepresentation(s, _isDirected),
            _ => throw new InvalidRepresentationTypeException()
        };
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

    /// <summary>
    /// Changes graph representation to adjacency matrix.
    /// </summary>
    public void ChangeToMatrixRepresentation()
    {
        if (_representationType == RepresentationTypeEnum.MATRIX)
        {
            return;
        }

        MatrixGraphRepresentation newRepresentation = new(VertexCount);
        CopyEdgesToNewRepresentation(newRepresentation);
        
        _representation = newRepresentation;
    }

    /// <summary>
    /// Changes graph representation to adjacency list.
    /// </summary>
    public void ChangeToListRepresentation()
    {
        if (_representationType == RepresentationTypeEnum.LIST)
        {
            return;
        }

        ListGraphRepresentation newRepresentation = new(VertexCount);
        CopyEdgesToNewRepresentation(newRepresentation);
        
        _representation = newRepresentation;
    }

    /// <summary>
    /// Changes graph representation basing on given type.
    /// </summary>
    /// <param name="representationType">Type of graph representation to change.</param>
    /// <exception cref="InvalidRepresentationTypeException">Thrown if given representation type is invalid.</exception>
    public void ChangeRepresentation(RepresentationTypeEnum representationType)
    {
        switch (representationType) 
        {
            case RepresentationTypeEnum.LIST: 
                ChangeToListRepresentation();
                break;
            case RepresentationTypeEnum.MATRIX:
                ChangeToMatrixRepresentation();
                break;
            default:
                throw new InvalidRepresentationTypeException();
        };
    }

    /// <summary>
    /// Copies the edges with their weight to a new instance of graph representation.
    /// </summary>
    /// <param name="newRepresentation">An object of new graph representation.</param>
    private void CopyEdgesToNewRepresentation(GraphRepresentation newRepresentation)
    {
        for (int i = 0; i < VertexCount; i++)
        {
            var neighbours = _representation.GetNeighbours(i);

            foreach (var n in neighbours)
            {
                newRepresentation.AddEdge(i, n);
            }
        }
    }
}


/// <summary>
/// Abstract class for weighted graphs objects.
/// </summary>
/// <typeparam name="T">Type of edges weights.</typeparam>
abstract public class GraphBase<T> : IWeightedGraphMethods<T> where T : INumber<T>
{
    protected GraphRepresentation<T> _representation;
    protected RepresentationTypeEnum _representationType;
    protected bool _isDirected;
    public GraphBase(int V, RepresentationTypeEnum representationType, bool isDirected)
    {
        _isDirected = isDirected;
        _representationType = representationType;
        _representation = representationType switch
        {
            RepresentationTypeEnum.LIST => new ListGraphRepresentation<T>(V),
            RepresentationTypeEnum.MATRIX => new MatrixGraphRepresentation<T>(V),
            _ => throw new InvalidRepresentationTypeException()
        };
    }
    public GraphBase(Stream s, RepresentationTypeEnum representationType, bool isDirected)
    {
        _isDirected = isDirected;
        _representationType = representationType;
        _representation = representationType switch
        {
            RepresentationTypeEnum.LIST => new ListGraphRepresentation<T>(s, _isDirected),
            RepresentationTypeEnum.MATRIX => new MatrixGraphRepresentation<T>(s, _isDirected),
            _ => throw new InvalidRepresentationTypeException()
        };
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

    /// <summary>
    /// Changes graph representation to adjacency matrix.
    /// </summary>
    public void ChangeToMatrixRepresentation()
    {
        if (_representationType == RepresentationTypeEnum.MATRIX)
        {
            return;
        }

        MatrixGraphRepresentation<T> newRepresentation = new(VertexCount);
        CopyEdgesToNewRepresentation(newRepresentation);
        
        _representation = newRepresentation;
    }

    /// <summary>
    /// Changes graph representation to adjacency list.
    /// </summary>
    public void ChangeToListRepresentation()
    {
        if (_representationType == RepresentationTypeEnum.LIST)
        {
            return;
        }

        ListGraphRepresentation<T> newRepresentation = new(VertexCount);
        CopyEdgesToNewRepresentation(newRepresentation);
        
        _representation = newRepresentation;
    }

    /// <summary>
    /// Changes graph representation basing on given type.
    /// </summary>
    /// <param name="representationType">Type of graph representation to change.</param>
    /// <exception cref="InvalidRepresentationTypeException">Thrown if given representation type is invalid.</exception>
    public void ChangeRepresentation(RepresentationTypeEnum representationType)
    {
        switch (representationType) 
        {
            case RepresentationTypeEnum.LIST: 
                ChangeToListRepresentation();
                break;
            case RepresentationTypeEnum.MATRIX:
                ChangeToMatrixRepresentation();
                break;
            default:
                throw new InvalidRepresentationTypeException();
        };
    }

    /// <summary>
    /// Copies the edges with their weight to a new instance of graph representation.
    /// </summary>
    /// <param name="newRepresentation">An object of new graph representation.</param>
    private void CopyEdgesToNewRepresentation(GraphRepresentation<T> newRepresentation)
    {
        for (int i = 0; i < VertexCount; i++)
        {
            var edges = _representation.GetOutEdges(i);

            foreach (var edge in edges)
            {
                newRepresentation.AddEdge(i, edge.Item1);
                newRepresentation.SetEdgeWeight(i, edge.Item1, edge.Item2);
            }
        }
    }
}