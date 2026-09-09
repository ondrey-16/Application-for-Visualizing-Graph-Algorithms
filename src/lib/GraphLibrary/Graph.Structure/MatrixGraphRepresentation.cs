namespace AVGA.GraphLibrary;

public class MatrixGraphRepresentation<T> : GraphRepresentation<T> where T : INumber<T>
{
    private readonly (bool, T)[,] _graph;

    public MatrixGraphRepresentation(int V) : base(V)
    {
        _graph = new (bool, T)[_V,_V];
    }
    public override bool AddEdge(int u, int v)
    {
        CheckEdge(u, v);

        if (!_graph[u, v].Item1)
        {
            _graph[u, v] = (true, T.Zero);
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

        if (_graph[u, v].Item1)
        {
            _graph[u, v].Item1 = false;
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

        if (_graph[u, v].Item1)
        {
            return _graph[u, v].Item2;
        }

        throw new NonExistingEdgeException(u, v);
    }

    public override IEnumerable<int> GetNeighbours(int v)
    {
        CheckVertex(v);

        for (int i = 0; i < _V; i++)
        {
            if (_graph[v, i].Item1)
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
            if (_graph[v, i].Item1)
            {
                yield return (i, _graph[v, i].Item2);
            }
        }
    }

    public override void SetEdgeWeight(int u, int v, T w)
    {
        CheckEdge(u, v);

        if (!_graph[u, v].Item1)
        {
            throw new NonExistingEdgeException(u, v);
        }

        _graph[u, v].Item2 = w;
    }

    public override bool HasEdge(int u, int v)
    {
        CheckEdge(u, v);
        
        return _graph[u, v].Item1;
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
                cloned._graph[i, j] = this._graph[i, j];
            }
        }

        cloned._E = this._E;

        return cloned;
    }
}