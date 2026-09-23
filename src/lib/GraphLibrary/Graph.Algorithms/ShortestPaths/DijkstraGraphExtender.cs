namespace AVGA.GraphLibrary;

/// <summary>
/// Class extending weighted graphs on Dijkstra algorithm.
/// </summary>
public static class DijkstraGraphExtender
{
    /// <summary>
    /// Finds shortest paths from source vertex to all reachable vertices using Dijkstra algorithm operating
    /// on weighted graphs without a negative-weighted edges.
    /// </summary>
    /// <typeparam name="T">Type of edges weights.</typeparam>
    /// <param name="graph">Weighted graph</param>
    /// <param name="source">A vertex from which shortest paths are being found.</param>
    /// <param name="paths">Object of found paths information.</param>
    /// <returns>Object with found shortest paths information.</returns>
    /// <exception cref="ArgumentException">Thrown if there was an error during dequeue process.</exception>
    /// <exception cref="NegativeEdgeWeightException">Thrown if there a negative-weighted edge was detected.</exception>
    public static Paths<T> Dijkstra<T>(this GraphBase<T> graph, int source, Paths<T>? paths = null) where T : INumber<T>, IMinMaxValue<T>
    {
        PriorityQueue<int, T> Q = new();
        if (paths is null)
        {
            paths = new(graph.VertexCount);
        }

        paths.InitSourceRow(source);
        paths.AddPathNode(source, source, source, T.Zero);

        Q.Enqueue(source, T.Zero);

        while (Q.Count > 0)
        {
            if (!Q.TryDequeue(out int u, out T? w))
            {
                throw new ArgumentException("Cannot dequeue an element.");
            }
            if (w > paths.GetDistance(source, u))
            {
                continue;
            }

            foreach (var edge in graph.GetOutEdges(u))
            {
                if (edge.Weight < T.Zero)
                {
                    throw new NegativeEdgeWeightException();
                }
                if (paths.IsReachable(source, u))
                {
                    if (!paths.IsReachable(source, edge.To) || 
                        paths.GetDistance(source, edge.To) > paths.GetDistance(source, u) + edge.Weight)
                    {
                        paths.AddPathNode(source, edge.To, u, paths.GetDistance(source, u) + edge.Weight);
                        Q.Enqueue(edge.To, paths.GetDistance(source, edge.To));
                    }
                }
            }
        }

        return paths;
    }
}