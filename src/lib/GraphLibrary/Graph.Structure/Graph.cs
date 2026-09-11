namespace AVGA.GraphLibrary;

public abstract class Graph<T> : IGraphMethods<T> where T : INumber<T>
{
    protected RepresentationTypeEnum _representationType;

    public abstract int VertexCount { get; }

    public abstract int EdgeCount { get; }

    public abstract bool AddEdge(int u, int v);

    public abstract bool RemoveEdge(int u, int v);

    public abstract int GetInDegree(int v);

    public abstract int GetOutDegree(int v);

    public abstract IEnumerable<int> GetNeighbours(int v);

    public abstract IEnumerable<(int, T)> GetOutEdges(int v);

    public abstract bool HasEdge(int u, int v);

    public abstract object Clone();
    public void ChangeRepresentation(RepresentationTypeEnum representationType)
    {
        switch (representationType) 
        {
            case RepresentationTypeEnum.LIST: 
                ChangeToListRepresentation();
                break;
            case RepresentationTypeEnum.MATRIX:
                ChangeToMatrixRepresentation();
                break;
            default:
                throw new InvalidRepresentationTypeException();
        };
    }

    public abstract void ChangeToMatrixRepresentation();

    public abstract void ChangeToListRepresentation();
}