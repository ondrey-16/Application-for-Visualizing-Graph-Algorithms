namespace AVGA.GraphLibrary;

/// <summary>
/// Representation of graphs operating on adjacency list.
/// </summary>
/// <typeparam name="T">Type of edges weights.</typeparam>
public class ListGraphRepresentation<T> : GraphRepresentation<T> where T : INumber<T>
{
    private readonly List<List<(int, T)>> _adjacencyList;


    public ListGraphRepresentation(int V) : base(V)
    {
        _adjacencyList = Enumerable.Range(0, V).Select(_ => new List<(int, T)>()).ToList();
    }

    public ListGraphRepresentation(Stream s, bool isDirected) : base()
    {
        _adjacencyList = new List<List<(int, T)>>();

        string? line;
        int u, v;
        T w = T.One;
        int maxV = 0;

        using StreamReader sr = new(s);

        while ((line = sr.ReadLine()) is not null)
        {
            var separated = line.Split( ).ToList();
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
                    _adjacencyList.Add(new List<(int, T)>());
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

            _adjacencyList[u].Add((v, w));
            _inDegrees[v]++;
            _outDegrees[u]++;
            _E++;

            if (!isDirected)
            {
                _adjacencyList[v].Add((u, w));
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

        if (!_adjacencyList[u].Any(e => e.Item1 == v))
        {
            _adjacencyList[u].Add((v, T.Zero));
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

        if (_adjacencyList[u].Any(e => e.Item1 == v))
        {
            _adjacencyList[u].RemoveAll(e => e.Item1 == v);
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
        
        if (_adjacencyList[u].Any(e => e.Item1 == v))
        {
            return _adjacencyList[u].FirstOrDefault(e => e.Item1 == v).Item2;
        }

        throw new NonExistingEdgeException(u, v);
    }

    public override IEnumerable<int> GetNeighbours(int v)
    {
        CheckVertex(v);

        return _adjacencyList[v].Select(e => e.Item1);
    }

    public override IEnumerable<(int, T)> GetOutEdges(int v)
    {
        CheckVertex(v);

        foreach (var edge in _adjacencyList[v])
        {
            yield return edge;
        }
    }

    public override void SetEdgeWeight(int u, int v, T w)
    {
        CheckEdge(u, v);

        for (int i = 0; i < _adjacencyList[u].Count; i++)
        {
            if (_adjacencyList[u][i].Item1 == v)
            {
                _adjacencyList[u][i] = (v, w);
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
        
        return _adjacencyList[u].Any(e => e.Item1 == v);
    }

    public override object Clone()
    {
        ListGraphRepresentation<T> cloned = new(_V);

        for (int i = 0; i < this._V; i++)
        {
            cloned._inDegrees[i] = this._inDegrees[i];
            cloned._outDegrees[i] = this._outDegrees[i];
            cloned._adjacencyList[i] = new List<(int, T)>();

            for (int j = 0; j < this._adjacencyList[i].Count; j++)
            {
                cloned._adjacencyList[i].Add(this._adjacencyList[i][j]);
            }
        }

        cloned._E = this._E;

        return cloned;
    }
}