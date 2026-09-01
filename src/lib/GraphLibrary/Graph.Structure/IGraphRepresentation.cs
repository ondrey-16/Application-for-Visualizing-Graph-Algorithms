namespace AVGA.GraphLibrary;

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
    /// Adds the edge to graph's representation if doesn't exist.
    /// </summary>
    /// <param name="u">Start of the edge.</param>
    /// <param name="v">End of the edge.</param>
    /// <param name="w">Weight of the edge</param>
    /// <returns>If edge added successfully.</returns>
    public bool AddEdge(int u, int v, T w);

    /// <summary>
    /// Removes the edge if exists.
    /// </summary>
    /// <param name="u">Start of the edge.</param>
    /// <param name="v">End of the edge.</param>
    /// <returns>If edge removed successfully.</returns> 
    public bool RemoveEdge(int u, int v);

    /// <summary>
    /// Returns a weight of the edge if exists.
    /// </summary>
    /// <param name="u">Start of the edge.</param>
    /// <param name="v">End of the edge.</param>
    /// <returns>Weight of the edge</returns>
    public T GetEdgeWeight(int u, int v);

    /// <summary>
    /// Returns a count of incoming edges of vertex.
    /// </summary>
    /// <param name="v">Vertex</param>
    /// <returns>Count of incoming edges of vertex.</returns>
    /// 
    public int GetInDegree(int v);

    /// <summary>
    /// Returns a count of outgoing edges of vertex.
    /// </summary>
    /// <param name="v">Vertex</param>
    /// <returns>Count of outgoing edges of vertex.</returns>
    public int GetOutDegree(int v);

    /// <summary>
    /// Returns neighbours of the vertex.
    /// </summary>
    /// <param name="v">Vertex</param>
    /// <returns>Collection of neighbours of the vertex.</returns>
    public IEnumerable<int> GetNeighbours(int v);

    /// <summary>
    /// Returns outgoing edges of vertex.
    /// </summary>
    /// <param name="v">Vertex</param>
    /// <returns>Collection of neighbours of the vertex.</returns>
    public IEnumerable<(int, T)> GetOutEdges(int v);

    /// <summary>
    /// Sets edge's weight if exists.
    /// </summary>
    /// <param name="v">Vertex</param>
    public void SetEdgeWeight(int u, int v, T w);
    /// <summary>
    /// Returns information if edge exists.
    /// </summary>
    /// <param name="u">Start of the edge.</param>
    /// <param name="v">End of the edge.</param>
    /// <returns>If edge exists.</returns>
    public bool HasEdge(int u, int v);
}