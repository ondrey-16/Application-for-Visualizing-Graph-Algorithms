namespace AVGA.GraphLibrary;

public static class GetPathDFSGraphExtender
{
    /// <summary>
    /// Finds a path connecting provided vertices.
    /// </summary>
    /// <param name="graph">Unweighted graph.</param>
    /// <param name="source">Source vertex.</param>
    /// <param name="target">Target vertex.</param>
    /// <returns>Found path as List of integers. A source vertex if is equal to the target or empty List if a path does not exist.</returns>
    public static List<int> GetPathDFS(this GraphBase graph, int source, int target)
    {
        if (source == target)
        {
            return new List<int>{source};
        }
        Paths<int> paths = new(graph.VertexCount);
        paths.InitSourceRow(source);
        paths.AddPathNode(source, source, source, 0);

        foreach (var edge in graph.DFS().SearchFrom(source))
        {
            if (!paths.IsReachable(source, edge.To))
            {
                paths.AddPathNode(source, edge.To, edge.From, paths.GetDistance(source, edge.From) + 1);
                if (edge.To == target)
                {
                    break;
                }
            }
        }

        if (!paths.IsReachable(source, target))
        {
            return new List<int>();
        }

        return paths.GetPath(source, target);
    }

    /// <summary>
    /// Finds a path connecting provided vertices.
    /// </summary>
    /// <param name="graph">Weighted graph.</param>
    /// <param name="source">Source vertex.</param>
    /// <param name="target">Target vertex.</param>
    /// <returns>Found path as List of integers. A source vertex if is equal to the target or empty List if a path does not exist.</returns>
    public static List<int>? GetPathDFS<T>(this GraphBase<T> graph, int source, int target)
    {
        if (source == target)
        {
            return new List<int>{source};
        }
        Paths<int> paths = new(graph.VertexCount);
        paths.InitSourceRow(source);
        paths.AddPathNode(source, source, source, 0);

        foreach (var edge in graph.DFS().SearchFrom(source))
        {
            if (!paths.IsReachable(source, edge.To))
            {
                paths.AddPathNode(source, edge.To, edge.From, paths.GetDistance(source, edge.From) + 1);
                if (edge.To == target)
                {
                    break;
                }
            }
        }

        if (!paths.IsReachable(source, target))
        {
            return null;
        }

        return paths.GetPath(source, target);
    }
}