namespace AVGA.GraphLibrary;

/// <summary>
/// Representation of unweighted graphs operating on adjacency dictionary.
/// </summary>
public class DictionaryRepresentation : IGraphMethods, ICloneable
{
    private Dictionary<int, List<int>> _adjacencyDictionary;
    private readonly List<int> _inDegrees;
    private readonly List<int> _outDegrees;
    public int VertexCount { get; }
    public int EdgeCount { get; private set; }
    public Dictionary<int, List<int>> Representation { get => _adjacencyDictionary; }

    /// <summary>
    /// Constructs an object with representation of graphs reserved for V vertices.
    /// </summary>
    /// <param name="V">Count of vertices.</param>
    public DictionaryRepresentation(int V)
    {
        VertexCount = V;
        _inDegrees = Enumerable.Repeat(0, V).ToList();
        _outDegrees = Enumerable.Repeat(0, V).ToList();
        EdgeCount = 0;
        _adjacencyDictionary = Enumerable
            .Range(0, V)
            .Select(e => (e, new List<int>()))
            .ToDictionary();
    }
    /// <summary>
    /// Constructs an object with representation of graphs compatible with data read from stream.
    /// </summary>
    /// <param name="s">Graph data stream.</param>
    /// <exception cref="InvalidDataCountException">Thrown when in read data line there is an invalid count of balid variables.</exception>
    /// <exception cref="InvalidReadTypeException">Thrown when a variable cannot be parsed from read stream.</exception>
    public DictionaryRepresentation(Stream s)
    {
        _inDegrees = new();
        _outDegrees = new();
        _adjacencyDictionary = new();

        string? line;
        int u, v;
        int maxV = 0;

        using StreamReader sr = new(s);

        while ((line = sr.ReadLine()) is not null)
        {
            var separated = line.Split( ).ToList();
            if (separated.Count != 2)
            {
                throw new InvalidDataCountException(separated.Count, 2);
            }
            if (!int.TryParse(separated[0], out u))
            {
                throw new InvalidReadTypeException(separated[0]);
            }
            if (!int.TryParse(separated[1], out v))
            {
                throw new InvalidReadTypeException(separated[1]);
            }

            int maxL = Math.Max(u, v);

            if (maxL >= maxV)
            {
                for (int i = maxV; i <= maxL; i++)
                {
                    _adjacencyDictionary.Add(i, new List<int>());
                    _inDegrees.Add(0);
                    _outDegrees.Add(0);
                }

                maxV = maxL + 1;
            }

            if (HasEdge(u, v))
            {
                continue;
            }

            _adjacencyDictionary[u].Add(v);
            _inDegrees[v]++;
            _outDegrees[u]++;
            EdgeCount++;
        }

        VertexCount = maxV;
    }

    public bool AddEdge(int u, int v)
    {
        if (EdgeValidator.CheckIfValid(u, v, VertexCount) 
            && !_adjacencyDictionary[u].Any(e => e == v))
        {
            _adjacencyDictionary[u].Add(v);
            EdgeCount++;
            _inDegrees[v]++;
            _outDegrees[u]++;

            return true;
        }

        return false;
    }
    public bool RemoveEdge(int u, int v)
    {
        if (EdgeValidator.CheckIfValid(u, v, VertexCount) 
            && _adjacencyDictionary[u].Remove(v))
        {
            EdgeCount--;
            _inDegrees[v]--;
            _outDegrees[u]--;
            
            return true;
        }

        return false;
    }
    public int GetInDegree(int v)
    {
        VertexValidator.IsValid(v, VertexCount);
        return _inDegrees[v];
    }
    public int GetOutDegree(int v)
    {
        VertexValidator.IsValid(v, VertexCount);
        return _outDegrees[v];
    }
    public IEnumerable<int> GetNeighbours(int v)
    {
        VertexValidator.IsValid(v, VertexCount);
        return _adjacencyDictionary[v];
    }
    public bool HasEdge(int u, int v)
    {
        if (EdgeValidator.CheckIfValid(u, v, VertexCount))
        {
            return _adjacencyDictionary[u].Any(e => e == v);
        }

        return false;
    }
    public object Clone()
    {
        DictionaryRepresentation cloned = new(VertexCount);

        cloned._adjacencyDictionary = new(this._adjacencyDictionary);
        cloned.EdgeCount = this.EdgeCount;

        return cloned;
    }
}


/// <summary>
/// Representation of weighted graphs operating on adjacency dictionary.
/// </summary>
/// <typeparam name="T">Type of edges weights.</typeparam>
public class DictionaryRepresentation<T> : IWeightedGraphMethods<T>, ICloneable where T : INumber<T>
{
    private Dictionary<int, List<(int vertex, T weight)>> _adjacencyDictionary;
    private readonly List<int> _inDegrees;
    private readonly List<int> _outDegrees;
    public int VertexCount { get; }
    public int EdgeCount { get; private set; }
    public Dictionary<int, List<(int, T)>> Representation { get => _adjacencyDictionary; }

    /// <summary>
    /// Constructs an object with representation of weighted graphs reserved for V vertices.
    /// </summary>
    /// <param name="V">Count of vertices.</param>
    public DictionaryRepresentation(int V)
    {
        VertexCount = V;
        _inDegrees = Enumerable.Repeat(0, V).ToList();
        _outDegrees = Enumerable.Repeat(0, V).ToList();
        EdgeCount = 0;
        _adjacencyDictionary = Enumerable
            .Range(0, V)
            .Select(e => (e, new List<(int, T)>()))
            .ToDictionary();
    }
    /// <summary>
    /// Constructs an object with representation of weighted graphs compatible with data read from stream.
    /// </summary>
    /// <param name="s">Graph data stream.</param>
    /// <exception cref="InvalidDataCountException">Thrown when in read data line there is an invalid count of balid variables.</exception>
    /// <exception cref="InvalidReadTypeException">Thrown when a variable cannot be parsed from read stream.</exception>
    /// <exception cref="DuplicatedEdgeException">Thrown when an edge appears while reading more than one time.</exception>
    public DictionaryRepresentation(Stream s)
    {
        _inDegrees = new();
        _outDegrees = new();
        _adjacencyDictionary = new();

        string? line;
        int u, v;
        int maxV = 0;
        T w = T.Zero;

        using StreamReader sr = new(s);

        while ((line = sr.ReadLine()) is not null)
        {
            var separated = line.Split( ).ToList();
            if (separated.Count != 3)
            {
                throw new InvalidDataCountException(separated.Count, 3);
            }
            if (!int.TryParse(separated[0], out u))
            {
                throw new InvalidReadTypeException(separated[0]);
            }
            if (!int.TryParse(separated[1], out v))
            {
                throw new InvalidReadTypeException(separated[1]);
            }

            int maxL = Math.Max(u, v);

            if (maxL >= maxV)
            {
                for (int i = maxV; i <= maxL; i++)
                {
                    _adjacencyDictionary.Add(i, new List<(int, T)>());
                    _inDegrees.Add(0);
                    _outDegrees.Add(0);
                }

                maxV = maxL + 1;
            }

            if (HasEdge(u, v))
            {
                throw new DuplicatedEdgeException(u, v);
            }

            if (separated.Count == 3)
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));

                if (converter is not null)
                {
                    T? readW = (T?)converter.ConvertFromString(separated[2]);

                    if (readW is null)
                    {
                        throw new InvalidReadTypeException(separated[2]);
                    }
                    
                    w = readW;
                }
            }

            _adjacencyDictionary[u].Add((v, w));
            _inDegrees[v]++;
            _outDegrees[u]++;
            EdgeCount++;
        }

        VertexCount = maxV;
    }

    public bool AddEdge(int u, int v)
    {
        if (EdgeValidator.CheckIfValid(u, v, VertexCount) 
            && !_adjacencyDictionary[u].Any(e => e.vertex == v))
        {
            _adjacencyDictionary[u].Add((v, T.Zero));
            EdgeCount++;
            _inDegrees[v]++;
            _outDegrees[u]++;

            return true;
        }

        return false;
    }
    public bool RemoveEdge(int u, int v)
    {
        if (EdgeValidator.CheckIfValid(u, v, VertexCount) 
            && _adjacencyDictionary[u].Any(e => e.vertex == v))
        {
            _adjacencyDictionary[u].Remove(_adjacencyDictionary[u].First(e => e.vertex == v));
            EdgeCount--;
            _inDegrees[v]--;
            _outDegrees[u]--;
            
            return true;
        }

        return false;
    }
    public int GetInDegree(int v)
    {
        VertexValidator.IsValid(v, VertexCount);
        return _inDegrees[v];
    }
    public int GetOutDegree(int v)
    {
        VertexValidator.IsValid(v, VertexCount);
        return _outDegrees[v];
    }
    public IEnumerable<int> GetNeighbours(int v)
    {
        VertexValidator.IsValid(v, VertexCount);
        return _adjacencyDictionary[v].Select(e => e.vertex);
    }
    public T GetEdgeWeight(int u, int v)
    {
        EdgeValidator.IsValid(u, v, VertexCount);

        if (_adjacencyDictionary[u].Any(e => e.vertex == v))
        {
            return _adjacencyDictionary[u].FirstOrDefault(e => e.vertex == v).weight;
        }

        throw new NonExistingEdgeException(u, v);
    }
    public void SetEdgeWeight(int u, int v, T w)
    {
        EdgeValidator.IsValid(u, v, VertexCount);

        for (int i = 0; i < _adjacencyDictionary[u].Count; i++)
        {
            if (_adjacencyDictionary[u][i].vertex == v)
            {
                _adjacencyDictionary[u][i] = (v, w);
                return;
            }
        }

        throw new NonExistingEdgeException(u, v);
    }
    public bool HasEdge(int u, int v)
    {
        if (EdgeValidator.CheckIfValid(u, v, VertexCount))
        {
           return _adjacencyDictionary[u].Any(e => e.vertex == v);
        }

        return false;
    }
    public IEnumerable<(int, T)> GetOutEdges(int v)
    {
        VertexValidator.IsValid(v, VertexCount);
        return _adjacencyDictionary[v];
    }
    public object Clone()
    {
        DictionaryRepresentation<T> cloned = new(VertexCount);

        cloned._adjacencyDictionary = new(this._adjacencyDictionary);
        cloned.EdgeCount = this.EdgeCount;

        return cloned;
    }
}