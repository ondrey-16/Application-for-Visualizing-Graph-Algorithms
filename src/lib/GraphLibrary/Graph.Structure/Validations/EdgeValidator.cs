namespace AVGA.GraphLibrary;

/// <summary>
/// Used to check if edge is valid.
/// </summary>
internal static class EdgeValidator
{
    /// <summary>
    /// Checks if edge is valid. If not, throws an exception.
    /// </summary>
    /// <param name="u">Start of the edge.</param>
    /// <param name="v">End of the edge.</param>
    /// <param name="V">Count of vertices</param>
    /// <exception cref="InvalidEdgeException">Thrown if at least one of the vertices is out of range.</exception>
    internal static void IsValid(int u, int v, int V)
    {
        if (u < 0 || u >= V || v < 0 || v >= V)
        {
            throw new InvalidEdgeException(u, v, V);
        }
    }
    /// <summary>
    /// Checks if edge is valid.
    /// </summary>
    /// <param name="u">Start of the edge.</param>
    /// <param name="v">End of the edge.</param>
    /// <param name="V">Count of vertices</param>
    /// <returns>If edge is valid.</returns>
    internal static bool CheckIfValid(int u, int v, int V)
    {
        return u >= 0 && u < V && v >= 0 && v < V;
    }
}