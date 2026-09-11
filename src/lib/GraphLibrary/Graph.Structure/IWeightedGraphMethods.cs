namespace AVGA.GraphLibrary;

public interface IWeightedGraphMethods<T> where T : INumber<T>
{
    /// <summary>
    /// Returns a weight of the edge if exists.
    /// </summary>
    /// <param name="u">Start of the edge.</param>
    /// <param name="v">End of the edge.</param>
    /// <returns>Weight of the edge</returns>
    public T GetEdgeWeight(int u, int v);

    /// <summary>
    /// Sets edge's weight if exists.
    /// </summary>
    /// <param name="v">Vertex</param>
    /// <param name="w">Weight of the edge</param>
    public void SetEdgeWeight(int u, int v, T w);
}