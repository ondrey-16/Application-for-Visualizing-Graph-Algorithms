namespace AVGA.GraphLibrary;

public class ListGraphRepresentation<T> : GraphRepresentation<T> where T : INumber<T>
{
    private readonly List<List<(int, T)>> _graph;

    public ListGraphRepresentation(int V) : base(V)
    {
        _graph = Enumerable.Range(0, V).Select(_ => new List<(int, T)>()).ToList();
    }

    public ListGraphRepresentation(Stream s, bool isDirected) : base()
    {
        _graph = new List<List<(int, T)>>();

        string? line;
        int u, v;
        T w = T.One;
        int maxV = 0;

        using StreamReader sr = new(s);

        while ((line = sr.ReadLine()) is not null)
        {
            var separated = line.Split(' ').ToList();
            if (separated.Count < 2 || separated.Count > 3)
            {
                throw new ArgumentException();
            }

            if (!int.TryParse(separated[0], out u))
            {
                throw new ArgumentException();
            }
            if (!int.TryParse(separated[1], out v))
            {
                throw new ArgumentException();
            }

            int maxL = Math.Max(u, v);

            if (maxL >= maxV)
            {
                for (int i = maxV; i <= maxL; i++)
                {
                    _graph.Add(new List<(int, T)>());
                    _inDegrees.Add(0);
                    _outDegrees.Add(0);
                }

                maxV = maxL + 1;
            }

            if (separated.Count == 3)
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));

                if (converter is not null)
                {
                    T? readW = (T?)converter.ConvertFromString(separated[2]);

                    if (readW is null)
                    {
                        throw new ArgumentException();
                    }
                    
                    w = readW;
                }
            }

            if (HasEdge(u, v))
            {
                throw new ArgumentException();
            }

            _graph[u].Add((v, w));
            _inDegrees[v]++;
            _outDegrees[u]++;
            _E++;

            if (!isDirected)
            {
                _graph[v].Add((u, w));
                _inDegrees[u]++;
                _outDegrees[v]++;
                _E++;
            }
        }

        _V = maxV;
    }

    public override bool AddEdge(int u, int v)
    {
        CheckEdge(u, v);

        if (!_graph[u].Any(e => e.Item1 == v))
        {
            _graph[u].Add((v, T.Zero));
            _E++;
            _inDegrees[v]++;
            _outDegrees[u]++;

            return true;
        }

        return false;
    }

    public override bool RemoveEdge(int u, int v)
    {
        CheckEdge(u, v);

        if (_graph[u].Any(e => e.Item1 == v))
        {
            _graph[u].RemoveAll(e => e.Item1 == v);
            _E--;
            _inDegrees[v]--;
            _outDegrees[u]--;
            
            return true;
        }

        return false;
    }

    public override T GetEdgeWeight(int u, int v)
    {
        CheckEdge(u, v);
        
        if (_graph[u].Any(e => e.Item1 == v))
        {
            return _graph[u].FirstOrDefault(e => e.Item1 == v).Item2;
        }

        throw new NonExistingEdgeException(u, v);
    }

    public override IEnumerable<int> GetNeighbours(int v)
    {
        CheckVertex(v);

        return _graph[v].Select(e => e.Item1);
    }

    public override IEnumerable<(int, T)> GetOutEdges(int v)
    {
        CheckVertex(v);

        foreach (var edge in _graph[v])
        {
            yield return edge;
        }
    }

    public override void SetEdgeWeight(int u, int v, T w)
    {
        CheckEdge(u, v);

        for (int i = 0; i < _graph[u].Count; i++)
        {
            if (_graph[u][i].Item1 == v)
            {
                _graph[u][i] = (v, w);
                return;
            }
        }

        throw new NonExistingEdgeException(u, v);
    }

    public override bool HasEdge(int u, int v)
    {
        try
        {
            CheckEdge(u, v);
        }
        catch (InvalidEdgeException)
        {
            return false;
        }
        
        return _graph[u].Any(e => e.Item1 == v);
    }

    public override object Clone()
    {
        ListGraphRepresentation<T> cloned = new(_V);

        for (int i = 0; i < this._V; i++)
        {
            cloned._inDegrees[i] = this._inDegrees[i];
            cloned._outDegrees[i] = this._outDegrees[i];
            cloned._graph[i] = new List<(int, T)>(this._graph[i].Count);

            for (int j = 0; j < this._graph[i].Count; j++)
            {
                cloned._graph[i][j] = this._graph[i][j];
            }
        }

        cloned._E = this._E;

        return cloned;
    }
}