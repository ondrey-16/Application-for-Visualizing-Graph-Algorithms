namespace AVGA.GraphLibrary;

/// <summary>
/// Abstract class for graphs objects.
/// </summary>
/// <typeparam name="T">Type of edges weights.</typeparam>
abstract public class Graph<T> : IGraphMethods<T> where T : INumber<T>
{
    protected RepresentationTypeEnum _representationType;
    protected readonly bool _isDirected;

    public Graph(bool isDirected)
    {
        _isDirected = isDirected;
    }

    abstract public int VertexCount { get; }
    abstract public int EdgeCount { get; }
    abstract public bool AddEdge(int u, int v);
    abstract public bool RemoveEdge(int u, int v);
    abstract public int GetInDegree(int v);
    abstract public int GetOutDegree(int v);
    abstract public IEnumerable<int> GetNeighbours(int v);
    abstract public IEnumerable<(int, T)> GetOutEdges(int v);
    abstract public bool HasEdge(int u, int v);
    abstract public object Clone();

    /// <summary>
    /// Changes graph representation to adjacency matrix.
    /// </summary>
    abstract public void ChangeToMatrixRepresentation();

    /// <summary>
    /// Changes graph representation to adjacency list.
    /// </summary>
    abstract public void ChangeToListRepresentation();

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
}