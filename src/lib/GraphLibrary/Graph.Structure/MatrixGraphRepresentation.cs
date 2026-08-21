namespace AVGA.GraphLibrary
{
    public class MatrixGraphRepresentation<T> : IGraphRepresentation<T>
    {
        private readonly int _V;
        private int _E;
        private readonly (bool, T)[,] _graph;
        public MatrixGraphRepresentation(int V)
        {
            _V = V;
            _graph = new (bool, T)[_V,_V];
            _E = 0;
        }
        public int VertexCount => _V;

        public int EdgeCount => _E;

        public void AddEdge(int u, int v, T w)
        {
            _graph[u, v] = (true, w);
            _E++;
        }

        public int GetDegree(int v)
        {
            int deg = 0;
            for (int i = 0; i < _V; i++)
            {
                if (_graph[v, i].Item1)
                {
                    deg++;
                }
            }

            return deg;
        }

        public T? GetEdgeWeight(int u, int v) => _graph[u, v].Item2;
        public List<int> GetNeighbours(int v)
        {
            List<int> neighbours = new();

            for (int i = 0; i < _V; i++)
            {
                if (_graph[v, i].Item1)
                {
                    neighbours.Add(i);
                }
            }

            return neighbours;
        }

        public void RemoveEdge(int u, int v)
        {
            _graph[u, v].Item1 = false;
        }
    }
}