namespace AVGA.GraphLibrary;

/// <summary>
/// Class extending unweighted graphs on BFS algorithm for finding shortest paths.
/// </summary>
public static class BFSShortestPathsGraphExtender
{
    /// <summary>
    /// Finds shortest paths from source vertex to all reachable vertices using Breadth-First-Search algorithm operating
    /// on unweighted graphs. A distance of reachable vertices from the source corresponds to the smallest number of edges 
    /// connecting these vertices.
    /// </summary>
    /// <param name="graph">Unweighted graph</param>
    /// <param name="source">A vertex from which shortest paths are being found.</param>
    /// <param name="paths">Object of found paths information.</param>
    /// <returns>Object with found shortest paths information.</returns>
    public static Paths<int> BFSShortestPaths(this GraphBase graph, int source, Paths<int>? paths = null)
    {
        if (paths is null)
        {
            paths = new(graph.VertexCount);
        }

        paths.InitSourceRow(source);
        paths.AddPathNode(source, source, source, 0);

        Queue<int> Q = new();
        Q.Push(source);

        while (!Q.IsEmpty())
        {
            int u = Q.Pop();

            foreach (var edge in graph.GetOutEdges(u))
            {
                if (!paths.IsReachable(source, edge.To))
                {
                    paths.AddPathNode(source, edge.To, u, paths.GetDistance(source, u) + 1);
                    Q.Push(edge.To);
                }
            }
        }

        return paths;
    }
}

