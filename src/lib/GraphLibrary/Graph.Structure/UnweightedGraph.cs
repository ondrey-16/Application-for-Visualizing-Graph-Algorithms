namespace AVGA.GraphLibrary;

/// <summary>
/// Adapter on Graph class with edges weights represented by short int type variables, used to operate on unweighted graphs.
/// </summary>
public class UnweightedGraph : Graph<short>
{
    private WeightedGraph<short> _graph;


    public UnweightedGraph(int V, RepresentationTypeEnum representationType, bool isDirected) : base(isDirected)
    {
        _representationType = representationType;
        _graph = isDirected 
            ? new DirectedGraph<short>(V, representationType) 
            : new UndirectedGraph<short>(V, representationType);
    }

    public UnweightedGraph(Stream s, RepresentationTypeEnum representationType, bool isDirected) : base(isDirected)
    {
        _representationType = representationType;
        _graph = isDirected 
            ? new DirectedGraph<short>(s, representationType) 
            : new UndirectedGraph<short>(s, representationType);
    }

    public override int EdgeCount => _graph.EdgeCount;

    public override int VertexCount => _graph.VertexCount;

    public bool IsDirected => _isDirected;

    public override bool AddEdge(int u, int v)
    {
        bool res = _graph.AddEdge(u, v);
        
        if (res)
        {
            _graph.SetEdgeWeight(u, v, 1);
        }

        return res;
    }

    public override int GetInDegree(int v) => _graph.GetInDegree(v);

    public override IEnumerable<int> GetNeighbours(int v) => _graph.GetNeighbours(v);

    public override int GetOutDegree(int v) => _graph.GetOutDegree(v);

    public override IEnumerable<(int, short)> GetOutEdges(int v) => _graph.GetOutEdges(v);

    public override bool HasEdge(int u, int v) => _graph.HasEdge(u, v);

    public override bool RemoveEdge(int u, int v) => _graph.RemoveEdge(u, v);

    public override object Clone()
    {
        UnweightedGraph cloned = new (this.VertexCount, this._representationType, this._isDirected);
        cloned._graph = _isDirected 
            ? (DirectedGraph<short>) this._graph.Clone() 
            : (UndirectedGraph<short>) this._graph.Clone();

        return cloned;
    }

    public override void ChangeToMatrixRepresentation()
    {
        _graph.ChangeToMatrixRepresentation();
    }

    public override void ChangeToListRepresentation()
    {
        _graph.ChangeToListRepresentation();
    }
}