namespace AVGA.GraphLibrary;

public static class BFSShortestPathsGraphExtender
{
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

