namespace AVGA.GraphLibrary
{
    /// <summary>
    /// Interface of methods necessary to operate on graph's representations.
    /// </summary>
    /// <typeparam name="T">Type of edges' weights.</typeparam>
    public interface IGraphRepresentation<T>
    {
        /// <summary>
        /// Count of vertices building the graph.
        /// </summary>
        public int VertexCount { get; }
        /// <summary>
        /// Count of edges building the graph.
        /// </summary>
        public int EdgeCount { get; }
        /// <summary>
        /// Returns a weight of the edge if exists.
        /// </summary>
        /// <param name="u">Start of the edge.</param>
        /// <param name="v">End of the edge.</param>
        /// <returns>Weight of the edge or null</returns>
        public T? GetEdgeWeight(int u, int v);
        /// <summary>
        /// Adds the edge to graph's representation if doesn't exist.
        /// </summary>
        /// <param name="u">Start of the edge.</param>
        /// <param name="v">End of the edge.</param>
        /// <param name="w">Weight of the edge</param>
        public void AddEdge(int u, int v, T w);
        /// <summary>
        /// Removes the edge if exists.
        /// </summary>
        /// <param name="u">Start of the edge.</param>
        /// <param name="v">End of the edge.</param>
        public void RemoveEdge(int u, int v);
        /// <summary>
        /// Returns a degree of the vertex.
        /// </summary>
        /// <param name="v">Vertex</param>
        /// <returns>Degree of the vertex.</returns>
        public int GetDegree(int v);
        /// <summary>
        /// Returns neighbours of the vertex.
        /// </summary>
        /// <param name="v">Vertex</param>
        /// <returns>List of neighbours of the vertex.</returns>
        public List<int> GetNeighbours(int v);
    }
}