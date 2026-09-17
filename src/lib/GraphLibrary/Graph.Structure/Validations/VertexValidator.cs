namespace AVGA.GraphLibrary;

/// <summary>
/// Used to check if vertex is valid.
/// </summary>
internal static class VertexValidator
{
    /// <summary>
    /// Checks if vertex is valid. If not, throws an exception.
    /// </summary>
    /// <param name="v">Vertex number</param>
    /// <param name="V">Count of vertices</param>
    /// <exception cref="InvalidVertexException">Thrown if vertex is out of range.</exception>
    internal static void IsValid(int v, int V)
    {
        if (v < 0 || v >= V)
        {
            throw new InvalidVertexException(v, V);
        }
    }

    /// <summary>
    /// Checks if vertex is valid.
    /// </summary>
    /// <param name="u">Start of the edge.</param>
    /// <param name="v">End of the edge.</param>
    /// <param name="V">Count of vertices</param>
    /// <returns>If vertex is valid.</returns>
    internal static bool CheckIfValid(int v, int V)
    {
        return v >= 0 && v < V;
    }
}