namespace AVGA.GraphLibrary;

/// <summary>
/// Class with methods which searches a given graph to return edges in a correct order.
/// </summary>
public class GraphSearcher
{
    /// <summary>
    /// An array of enumerators iterating on a collecion of outgoing edges from vertices.
    /// </summary>
    private IEnumerator<Edge>[] _enumerators;
    /// <summary>
    /// A structure storing visited vertices in a right order.
    /// </summary>
    private IOrderStructure<int> _O;
    /// <summary>
    /// A searched graph.
    /// </summary>
    private GraphBase _graph;

    public GraphSearcher(IOrderStructure<int> O, GraphBase graph)
    {
        _O = O;
        _graph = graph;
        _enumerators = new IEnumerator<Edge>[graph.VertexCount];
    }

    /// <summary>
    /// Searches a whole graph by repeatedly called SearchFrom method.
    /// </summary>
    /// <returns>A collection of all edges in a correct order.</returns>
    public IEnumerable<Edge> SearchAllEdges()
    {
        for (int i = 0; i < _graph.VertexCount; i++)
        {
            if (_enumerators[i] is null)
            {
                foreach (var e in SearchFrom(i))
                {
                    yield return e;
                }
            }
        }
    }

    /// <summary>
    /// Searches a graph starting from a given vertex.
    /// </summary>
    /// <param name="v">Vertex.</param>
    /// <returns>A collection of searched edges in a correct order.</returns>
    public IEnumerable<Edge> SearchFrom(int v)
    {
        int currV, to;
        _enumerators[v] = _graph.GetOutEdges(v).GetEnumerator();
        _O.Push(v);

        try
        {
            while (!_O.IsEmpty())
            {
                currV = _O.Peek();
                to = _enumerators[currV].Current.To;

                if (!_enumerators[currV].MoveNext())
                {
                    _O.Pop();
                    continue;
                }

                if (_enumerators[_enumerators[currV].Current.To] is null)
                {
                    _enumerators[_enumerators[currV].Current.To] = _graph.GetOutEdges(_enumerators[currV].Current.To).GetEnumerator();
                    _O.Push(_enumerators[currV].Current.To);
                }

                yield return _enumerators[currV].Current;
            }
        }
        finally
        {
            foreach (var en in _enumerators)
            {
                if (en is not null)
                {
                    en.Dispose();
                }
            }
        }
    }
}


/// <summary>
/// Class with methods which searches a given weighted graph to return edges in a correct order.
/// </summary>
/// <typeparam name="T">Type of edges weight</typeparam>
public class GraphSearcher<T> where T : INumber<T>, IMinMaxValue<T>
{
    /// <summary>
    /// An array of enumerators iterating on a collecion of outgoing edges from vertices.
    /// </summary>
    private IEnumerator<Edge<T>>[] _enumerators;
    /// <summary>
    /// A structure storing visited vertices in a right order.
    /// </summary>
    private IOrderStructure<int> _O;
    /// <summary>
    /// A searched graph.
    /// </summary>
    private GraphBase<T> _graph;

    public GraphSearcher(IOrderStructure<int> O, GraphBase<T> graph)
    {
        _O = O;
        _graph = graph;
        _enumerators = new IEnumerator<Edge<T>>[graph.VertexCount];
    }

    /// <summary>
    /// Searches a whole graph by repeatedly called SearchFrom method.
    /// </summary>
    /// <returns>A collection of all edges in a correct order.</returns>
    public IEnumerable<Edge<T>> SearchAllEdges()
    {
        for (int i = 0; i < _graph.VertexCount; i++)
        {
            if (_enumerators[i] is null)
            {
                foreach (var e in SearchFrom(i))
                {
                    yield return e;
                }
            }
        }
    }

    /// <summary>
    /// Searches a graph starting from a given vertex.
    /// </summary>
    /// <param name="v">Vertex.</param>
    /// <returns>A collection of searched edges in a correct order.</returns>
    public IEnumerable<Edge<T>> SearchFrom(int v)
    {
        int currV;
        _enumerators[v] = _graph.GetOutEdges(v).GetEnumerator();
        _O.Push(v);

        try
        {
            while (!_O.IsEmpty())
            {
                currV = _O.Peek();

                if (!_enumerators[currV].MoveNext())
                {
                    _O.Pop();
                    continue;
                }

                if (_enumerators[_enumerators[currV].Current.To] is null)
                {
                    _enumerators[_enumerators[currV].Current.To] = _graph.GetOutEdges(_enumerators[currV].Current.To).GetEnumerator();
                    _O.Push(_enumerators[currV].Current.To);
                }

                yield return _enumerators[currV].Current;
            }
        }
        finally
        {
            foreach (var en in _enumerators)
            {
                if (en is not null)
                {
                    en.Dispose();
                }
            }
        }
    }
}