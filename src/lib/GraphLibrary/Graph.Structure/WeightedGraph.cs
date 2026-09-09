namespace AVGA.GraphLibrary;

public abstract class WeightedGraph<T> : Graph<T> where T : INumber<T>
{
    protected GraphRepresentation<T> _representation;

    public WeightedGraph(int V, RepresentationTypeEnum representationType)
    {
        _representationType = representationType;
        _representation = representationType switch
        {
            RepresentationTypeEnum.LIST => new ListGraphRepresentation<T>(V),
            RepresentationTypeEnum.MATRIX => new MatrixGraphRepresentation<T>(V),
            _ => throw new InvalidRepresentationTypeException()
        };
    }

    public WeightedGraph(Stream s, RepresentationTypeEnum representationType)
    {
        _representationType = representationType;
        _representation = new ListGraphRepresentation<T>(2);
    }

    public override int VertexCount => _representation.VertexCount;

    public override T GetEdgeWeight(int u, int v) => _representation.GetEdgeWeight(u, v);

    public override int GetInDegree(int v) => _representation.GetInDegree(v);

    public override int GetOutDegree(int v) => _representation.GetOutDegree(v);

    public override IEnumerable<int> GetNeighbours(int v)  => _representation.GetNeighbours(v);

    public override IEnumerable<(int, T)> GetOutEdges(int v) => _representation.GetOutEdges(v);

    public override bool HasEdge(int u, int v) => _representation.HasEdge(u, v);

    public override void SetEdgeWeight(int u, int v, T w)
    {
        _representation.SetEdgeWeight(u, v, w);
    }

    private void CopyEdgesToNewRepresentation(GraphRepresentation<T> newRepresentation)
    {
        for (int i = 0; i < VertexCount; i++)
        {
            var edges = _representation.GetOutEdges(i);

            foreach (var edge in edges)
            {
                newRepresentation.AddEdge(i, edge.Item1);
                newRepresentation.SetEdgeWeight(i, edge.Item1, edge.Item2);
            }
        }
    }

    public override void ChangeToMatrixRepresentation()
    {
        MatrixGraphRepresentation<T> newRepresentation = new(VertexCount);
        CopyEdgesToNewRepresentation(newRepresentation);
        
        _representation = newRepresentation;
    }

    public override void ChangeToListRepresentation()
    {
        ListGraphRepresentation<T> newRepresentation = new(VertexCount);
        CopyEdgesToNewRepresentation(newRepresentation);
        
        _representation = newRepresentation;
    }
}