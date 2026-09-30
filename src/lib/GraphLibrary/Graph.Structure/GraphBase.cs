namespace AVGA.GraphLibrary;

/// <summary>
/// Abstract class for unweighted graphs objects.
/// </summary>
/// <typeparam name="T">Type of edges weights.</typeparam>
abstract public class GraphBase : IGraphMethods
{
    /// <summary>
    /// A graph representation based on a adjacency dictionary.
    /// </summary>
    protected DictionaryRepresentation _representation;

    /// <summary>
    /// Constructs an empty graph reserved for V vertices.
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
    public IEnumerable<Edge> GetOutEdges(int v) => _representation.GetOutEdges(v);
    public bool HasEdge(int u, int v) => _representation.HasEdge(u, v);
}


/// <summary>
/// Abstract class for weighted graphs objects.
/// </summary>
/// <typeparam name="T">Type of edges weights.</typeparam>
abstract public class GraphBase<T> : IWeightedGraphMethods<T>
{
    /// <summary>
    /// A graph representation based on a adjacency dictionary.
    /// </summary>
    protected DictionaryRepresentation<T> _representation;

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
        _representation = new DictionaryRepresentation<T>(s, WeightReader);
    }

    abstract public int EdgeCount { get; }
    abstract public bool AddEdge(int u, int v, T w);
    abstract public bool RemoveEdge(int u, int v);
    abstract public object Clone();

    public int VertexCount => _representation.VertexCount;
    public int GetInDegree(int v) => _representation.GetInDegree(v);
    public int GetOutDegree(int v) => _representation.GetOutDegree(v);
    public IEnumerable<int> GetNeighbours(int v)  => _representation.GetNeighbours(v);
    public bool HasEdge(int u, int v) => _representation.HasEdge(u, v);
    public IEnumerable<Edge<T>> GetOutEdges(int v) => _representation.GetOutEdges(v);
    public T GetEdgeWeight(int u, int v) => _representation.GetEdgeWeight(u, v);
    public void SetEdgeWeight(int u, int v, T w) => _representation.SetEdgeWeight(u, v, w);

    /// <summary>
    /// Tries to convert edge weight value from text to provided type.
    /// </summary>
    /// <param name="weightParts">A list of string values representing edge weight.</param>
    /// <returns>A read value of edge weight.</returns>
    /// <exception cref="InvalidDataCountException">Thrown if there is an invalid weightParts count.</exception>
    /// <exception cref="InvalidReadTypeException">Thrown if a value was wrongly converted.</exception>
    /// <exception cref="TypeCannotBeReadException{T}">Thrown if a value cannot be converted to provided type.</exception>
    protected virtual T WeightReader(List<string> weightParts)
    {
        if (weightParts.Count != 1)
        {
            throw new InvalidDataCountException(weightParts.Count + 2, 3);
        }

        var converter = TypeDescriptor.GetConverter(typeof(T));

        if (converter is not null)
        {
            T? readW = (T?)converter.ConvertFromString(weightParts[0]);

            if (readW is null)
            {
                throw new InvalidReadTypeException(weightParts[0]);
            }
            
            return readW;
        }
        
        throw new TypeCannotBeReadException<T>();
    }
}