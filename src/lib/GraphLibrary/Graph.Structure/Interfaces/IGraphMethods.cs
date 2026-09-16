namespace AVGA.GraphLibrary;

/// <summary>
/// Interface of methods necessary to operate on graphs and their representations.
/// </summary>
public interface IGraphMethods
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
    /// <returns>If edge added successfully.</returns>
    public bool AddEdge(int u, int v);

    /// <summary>
    /// Removes the edge if exists.
    /// </summary>
    /// <param name="u">Start of the edge.</param>
    /// <param name="v">End of the edge.</param>
    /// <returns>If edge removed successfully.</returns> 
    public bool RemoveEdge(int u, int v);

    /// <summary>
    /// Returns a count of incoming edges of vertex.
    /// </summary>
    /// <param name="v">Vertex</param>
    /// <returns>Count of incoming edges of vertex.</returns>
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
    /// Returns information if edge exists.
    /// </summary>
    /// <param name="u">Start of the edge.</param>
    /// <param name="v">End of the edge.</param>
    /// <returns>If edge exists.</returns>
    public bool HasEdge(int u, int v);
}