namespace AVGA.GraphLibrary;

public abstract class Graph<T> : IGraphMethods<T> where T : INumber<T>
{
    public abstract int VertexCount { get; }

    public abstract int EdgeCount { get; }

    public abstract bool AddEdge(int u, int v);

    public abstract bool RemoveEdge(int u, int v);

    public abstract T GetEdgeWeight(int u, int v);

    public abstract int GetInDegree(int v);

    public abstract int GetOutDegree(int v);

    public abstract IEnumerable<int> GetNeighbours(int v);

    public abstract IEnumerable<(int, T)> GetOutEdges(int v);

    public abstract void SetEdgeWeight(int u, int v, T w);

    public abstract bool HasEdge(int u, int v);

    public abstract object Clone();
}