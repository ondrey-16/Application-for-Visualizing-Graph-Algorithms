namespace AVGA.GraphLibrary;

public class ListGraphRepresentation<T> : GraphRepresentation<T>
{
    private readonly List<List<(int, T)>> _graph;

    public ListGraphRepresentation(int V) : base(V)
    {
        _graph = new List<List<(int, T)>>();

        for (int i = 0; i < V; i++)
        {
            _graph.Add(new List<(int, T)>());
        }
    }

    public override bool AddEdge(int u, int v, T w)
    {
        CheckEdge(u, v);

        if (!_graph[u].Any(e => e.Item1 == v))
        {
            _graph[u].Add((v, w));
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
                _graph[u].RemoveAt(i);
                _graph[u].Add((v, w));

                return;
            }
        }

        throw new NonExistingEdgeException(u, v);
    }

    public override bool HasEdge(int u, int v)
    {
        CheckEdge(u, v);
        
        return _graph[u].Any(e => e.Item1 == v);
    }
}