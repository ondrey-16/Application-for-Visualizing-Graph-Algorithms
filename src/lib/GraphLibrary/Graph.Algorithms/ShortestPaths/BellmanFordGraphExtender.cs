namespace AVGA.GraphLibrary;

/// <summary>
/// Class extending weighted graphs on Bellman-Ford algorithm.
/// </summary>
public static class BellmanFordGraphExtender
{
    /// <summary>
    /// Finds shortest paths from source vertex to all reachable vertices using Bellman-Ford algorithm operating
    /// on weighted graphs without a negative-weighted cycle in their structure.
    /// </summary>
    /// <typeparam name="T">Type of edges weights.</typeparam>
    /// <param name="graph">Weighted graph</param>
    /// <param name="source">A vertex from which shortest paths are being found.</param>
    /// <param name="paths">Object of found paths information.</param>
    /// <returns>Object with found shortest paths information.</returns>
    /// <exception cref="NegativeCycleException">Thrown when a nagative cycle was detected.</exception>
    public static Paths<T> BellmanFord<T>(this GraphBase<T> graph, int source, Paths<T>? paths = null) where T : INumber<T>, IMinMaxValue<T>
    {
        if (paths is null)
        {
            paths = new(graph.VertexCount);
        }

        paths.InitSourceRow(source);
        paths.AddPathNode(source, source, source, T.Zero);

        bool change = true;

        for (int i = 1; i < graph.VertexCount && change; i++)
        {
            change = false;
            for (int v = 0; v < graph.VertexCount; v++)
            {
                foreach (var edge in graph.GetOutEdges(v))
                {
                    if (paths.IsReachable(source, edge.From))
                    {
                        if (!paths.IsReachable(source, edge.To) ||
                            paths.GetDistance(source, edge.To) > paths.GetDistance(source, edge.From) + edge.Weight)
                        {
                            paths.AddPathNode(source, edge.To, edge.From, paths.GetDistance(source, edge.From) + edge.Weight);
                            change = true;
                        }
                    }
                }
            }
        }
        for (int v = 0; v < graph.VertexCount; v++)
        {
            foreach (var edge in graph.GetOutEdges(v))
            {
                if (paths.GetDistance(source, edge.To) > paths.GetDistance(source, edge.From) + edge.Weight)
                {
                    throw new NegativeCycleException();
                }
            }
        }

        return paths;
    }
}