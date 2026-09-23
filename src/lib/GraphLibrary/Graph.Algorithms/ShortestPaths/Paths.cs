namespace AVGA.GraphLibrary;

/// <summary>
/// Class storing information about found shortest paths on given graph.
/// </summary>
/// <typeparam name="T">Type of distance values</typeparam>
public class Paths<T> where T : INumber<T>, IMinMaxValue<T>
{
    /// <summary>
    /// Two-dimensional array of vertices preceding on a path. For example, a value of _prev[i][j] tells us 
    /// what vertex precedes a j-vertex on a path starting i-vertex. _prev[i][j] = -1 means that there is no previous vertex.
    /// </summary>
    private int[][] _prev;
    /// <summary>
    /// Two-dimensional array of shortest distances from one vertex to another. For example, a value of _dist[i][j] tells us 
    /// what distance is on a path from i-vertex to j-vertex.
    /// </summary>
    private T[][] _dist;
    /// <summary>
    /// Count of vertices.
    /// </summary>
    private int _V;

    public Paths(int V)
    {
        _V = V;
        _prev = new int[V][];
        _dist = new T[V][];
    }

    /// <summary>
    /// Initialize a source-row of _prev and _dist arrays.
    /// </summary>
    /// <param name="source"></param>
    public void InitSourceRow(int source)
    {
        if (_prev[source] is null)
        {
            _prev[source] = new int[_V];
            _dist[source] = new T[_V];

            for (int i = 0; i < _V; i++)
            {
                _prev[source][i] = -1;
                _dist[source][i] = T.MaxValue;
            }
        }
    }

    /// <summary>
    /// Initialize all rows of _prev and _dist arrays.
    /// </summary>
    public void InitAllRows()
    {
        for (int i = 0; i < _V; i++)
        {
            InitSourceRow(i);
        }
    }

    /// <summary>
    /// Adds a path node initializing _prev[from][to] and _dist[from][to] by the previous and distance values.
    /// Initializes right arrays rows if were not.
    /// </summary>
    /// <param name="from">A vertex starting a path.</param>
    /// <param name="to">A vertex ending a path.</param>
    /// <param name="previous">Preceding vertex.</param>
    /// <param name="distance">Distance on a path.</param>
    public void AddPathNode(int from, int to, int previous, T distance)
    {
        PathValidator.AreCoordinatesValid(from, to, _V);

        if (_prev[from] is null)
        {
            InitSourceRow(from);
        }

        _prev[from][to] = previous;
        _dist[from][to] = distance;
    }

    /// <summary>
    /// Return a distance on a path between given vertices.
    /// </summary>
    /// <param name="from">A vertex starting a path.</param>
    /// <param name="to">A vertex ending a path.</param>
    /// <returns>Value of a distance.</returns>
    /// <exception cref="MissingPathsDataInformationException">Thrown when a "from" row was uninitialized.</exception>
    /// <exception cref="NonExistingPathException"Thrown when a path does not exist.></exception>
    public T GetDistance(int from, int to)
    {
        PathValidator.AreCoordinatesValid(from, to, _V);
        if (_dist[from] is null)
        {
            throw new MissingPathsDataInformationException(from);
        }
        if (_prev[from][to] == -1)
        {
            throw new NonExistingPathException(from, to);
        }

        return _dist[from][to];
    }

    /// <summary>
    /// Return a preceding "to" vertex on a path.
    /// </summary>
    /// <param name="from">A vertex starting a path.</param>
    /// <param name="to">A vertex ending a path.</param>
    /// <returns>Number of a previous vertex.</returns>
    /// <exception cref="MissingPathsDataInformationException">Thrown when a "from" row was uninitialized.</exception>
    public int GetPrevious(int from, int to)
    {
        PathValidator.AreCoordinatesValid(from, to, _V);
        if (_prev[from] is null)
        {
            throw new MissingPathsDataInformationException(from);
        }

        return _prev[from][to];
    }

    /// <summary>
    /// Return a path between given vertices.
    /// </summary>
    /// <param name="from">A vertex starting a path.</param>
    /// <param name="to">A vertex ending a path.</param>
    /// <returns>A list of ordered vertices. An empty list if given vertices are equal.</returns>
    /// <exception cref="MissingPathsDataInformationException">Thrown when a "from" row was uninitialized.</exception>
    /// <exception cref="NonExistingPathException"Thrown when a path does not exist.></exception>
    /// <exception cref="InfiniteCycleException">Thrown when an infinite cycle detected.</exception>
    public List<int> GetPath(int from, int to)
    {
        PathValidator.AreCoordinatesValid(from, to, _V);
        if (_prev[from] is null)
        {
            throw new MissingPathsDataInformationException(from);
        }
        if (_prev[from][to] == -1)
        {
            throw new NonExistingPathException(from, to);
        }

        if (from == to)
        {
            return new List<int>();
        }

        var path = new LinkedList<int>();
        path.AddFirst(to);

        int prev = _prev[from][to];
        int i = 1;
        while (prev != from)
        {
            path.AddFirst(prev);

            if (_prev[from][prev] == -1)
            {
                throw new NonExistingPathException(from, to);
            }

            prev = _prev[from][prev];
            if (++i > _V)
            {
                throw new InfiniteCycleException();
            }
        }

        path.AddFirst(from);

        return path.ToList();
    }

    /// <summary>
    /// Checks if a path between given vertices exist.
    /// </summary>
    /// <param name="from">A vertex starting a path.</param>
    /// <param name="to">A vertex ending a path.</param>
    /// <returns>True if is reachable, false in other way.</returns>
    public bool IsReachable(int from, int to)
    {
        PathValidator.AreCoordinatesValid(from, to, _V);
        if (_prev[from] is null)
        {
            return false;
        }
        if (from == to)
        {
            return true;
        }

        return _prev[from][to] != -1;
    }
}