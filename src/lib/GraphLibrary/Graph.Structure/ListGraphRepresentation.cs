namespace AVGA.GraphLibrary
{
    public class ListGraphRepresentation<T> : IGraphRepresentation<T>
    {
        private readonly int _V;
        private int _E;
        public int VertexCount => _V;
        public int EdgeCount => _E;
        private readonly List<List<(int, T)>> _graph;

        public ListGraphRepresentation(int V)
        {
            _graph = new(V);
        }
        public void AddEdge(int u, int v, T w)
        {
            _graph[u].Add((v, w));
        }

        public int GetDegree(int v) => _graph[v].Count;

        public T? GetEdgeWeight(int u, int v) => _graph[u].First(e => e.Item1 == v).Item2;
        public List<int> GetNeighbours(int v) => _graph[v].Select(e => e.Item1).ToList();

        public void RemoveEdge(int u, int v)
        {
            _graph[u].RemoveAll(e => e.Item1 == v);
        }
    }
}