namespace AVGA.GraphLibrary;

/// <summary>
/// Representation of graphs operating on adjacency matrix.
/// </summary>
/// <typeparam name="T">Type of edges weights.</typeparam>
public class MatrixGraphRepresentation<T> : GraphRepresentation<T> where T : INumber<T>
{
    private readonly (bool, T)[,] _adjacencyMatrix;


    public MatrixGraphRepresentation(int V) : base(V)
    {
        _adjacencyMatrix = new (bool, T)[_V,_V];
    }
    public MatrixGraphRepresentation(Stream s, bool isDirected) : base()
    {
        _adjacencyMatrix = new (bool, T)[_V,_V];

        string? line;
        int u, v;
        T w = T.One;
        int maxV = 0;

        using StreamReader sr1 = new(s);

        while ((line = sr1.ReadLine()) is not null)
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
                    _inDegrees.Add(0);
                    _outDegrees.Add(0);
                }

                maxV = maxL + 1;
            }
        }

        _adjacencyMatrix = new (bool, T)[maxV, maxV];

        s.Position = 0;
        using StreamReader sr2 = new(s);

        while ((line = sr2.ReadLine()) is not null)
        {
            var separated = line.Split( ).ToList();

            int.TryParse(separated[0], out u);
            int.TryParse(separated[1], out v);

            int maxL = Math.Max(u, v);

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
                    else
                    {
                        w = readW;
                    }
                }
            }

            if (_adjacencyMatrix[u, v].Item1)
            {
                throw new ArgumentException();
            }

            _adjacencyMatrix[u, v] = (true, w);
            _inDegrees[v]++;
            _outDegrees[u]++;
            _E++;

            if (!isDirected)
            {
                _adjacencyMatrix[v, u] = (true, w);
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

        if (!_adjacencyMatrix[u, v].Item1)
        {
            _adjacencyMatrix[u, v] = (true, T.Zero);
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

        if (_adjacencyMatrix[u, v].Item1)
        {
            _adjacencyMatrix[u, v].Item1 = false;
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

        if (_adjacencyMatrix[u, v].Item1)
        {
            return _adjacencyMatrix[u, v].Item2;
        }

        throw new NonExistingEdgeException(u, v);
    }

    public override IEnumerable<int> GetNeighbours(int v)
    {
        CheckVertex(v);

        for (int i = 0; i < _V; i++)
        {
            if (_adjacencyMatrix[v, i].Item1)
            {
                yield return i;
            }
        }
    }

    public override IEnumerable<(int, T)> GetOutEdges(int v)
    {
        CheckVertex(v);

        for (int i = 0; i < _V; i++)
        {
            if (_adjacencyMatrix[v, i].Item1)
            {
                yield return (i, _adjacencyMatrix[v, i].Item2);
            }
        }
    }

    public override void SetEdgeWeight(int u, int v, T w)
    {
        CheckEdge(u, v);

        if (!_adjacencyMatrix[u, v].Item1)
        {
            throw new NonExistingEdgeException(u, v);
        }

        _adjacencyMatrix[u, v].Item2 = w;
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
        
        return _adjacencyMatrix[u, v].Item1;
    }

    public override object Clone()
    {
        MatrixGraphRepresentation<T> cloned = new(_V);

        for (int i = 0; i < this._V; i++)
        {
            cloned._inDegrees[i] = this._inDegrees[i];
            cloned._outDegrees[i] = this._outDegrees[i];

            for (int j = 0; j < this._V; j++)
            {
                cloned._adjacencyMatrix[i, j] = this._adjacencyMatrix[i, j];
            }
        }

        cloned._E = this._E;

        return cloned;
    }
}