namespace AVGA.GraphLibrary;

/// <summary>
/// Class extending weighted graphs on Bellman-Ford algorithm.
/// </summary>
public static class NegativeCycleGraphExtender
{
    /// <summary>
    /// Finds a negative cycle in provided weighted graph using modified version of Bellman-Ford algorithm which are reachable from provided vertex.
    /// </summary>
    /// <typeparam name="T">Type of edges weights.</typeparam>
    /// <param name="graph">Weighted graph</param>
    /// <param name="source">A vertex from which shortest paths are being found.</param>
    /// <returns>Found cycle as List of integers or null if a cycle does not exist.</returns>
    public static List<int>? NegativeCycleFromSource<T>(this GraphBase<T> graph, int source) where T : INumber<T>, IMinMaxValue<T>
    {
        Paths<T> paths = new(graph.VertexCount);

        paths.InitSourceRow(source);
        paths.AddPathNode(source, source, source, T.Zero);

        int changed = -1;

        for (int i = 1; i <= graph.VertexCount; i++)
        {
            changed = -1;
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
                            changed = edge.To;
                        }
                    }
                }
            }
        }
        
        if (changed == -1)
        {
            return null;
        }

        LinkedList<int> cycle = new();
        int u = changed;
        for (int i = 0; i < graph.VertexCount; i++)
        {
            u = paths.GetPrevious(source, u);
        }

        cycle.AddFirst(u);
        int currU = paths.GetPrevious(source, u);

        while (currU != u)
        {
            cycle.AddFirst(currU);
            currU = paths.GetPrevious(source,currU);
        }

        return cycle.ToList();
    }

    /// <summary>
    /// Finds a negative cycle in provided weighted graph using modified version of Bellman-Ford algorithm.
    /// </summary>
    /// <typeparam name="T">Type of edges weights.</typeparam>
    /// <param name="graph">Weighted graph</param>
    /// <returns>Found cycle as List of integers or null if a cycle does not exist.</returns>
    public static List<int>? NegativeCycle<T>(this GraphBase<T> graph) where T : INumber<T>, IMinMaxValue<T>
    {
        DirectedGraph<T> tmpGraph = new(graph.VertexCount + 1);

        for (int i = 0; i < graph.VertexCount; i++)
        {
            foreach (var edge in graph.GetOutEdges(i))
            {
                tmpGraph.AddEdge(edge.From, edge.To, edge.Weight);
            }
            tmpGraph.AddEdge(graph.VertexCount, i, T.Zero);
        }

        return tmpGraph.NegativeCycleFromSource(graph.VertexCount);
    }
}