namespace AVGA.GraphLibrary;

public class Paths<T> where T : INumber<T>, IMinMaxValue<T>
{
    private int[][] _prev;
    private T[][] _dist;
    private int _V;

    public Paths(int V)
    {
        _V = V;
        _prev = new int[V][];
        _dist = new T[V][];
    }

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

    public void InitAllRows()
    {
        for (int i = 0; i < _V; i++)
        {
            InitSourceRow(i);
        }
    }

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

    public int GetPrevious(int from, int to)
    {
        PathValidator.AreCoordinatesValid(from, to, _V);
        if (_prev[from] is null)
        {
            throw new MissingPathsDataInformationException(from);
        }

        return _prev[from][to];
    }

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