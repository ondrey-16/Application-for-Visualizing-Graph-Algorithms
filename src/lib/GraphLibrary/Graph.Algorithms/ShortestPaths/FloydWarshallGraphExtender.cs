namespace AVGA.GraphLibrary;

public static class FloydWarshallGraphExtender
{
    public static Paths<T> FloydWarshall<T>(this GraphBase<T> graph) where T : INumber<T>, IMinMaxValue<T>
    {
        Paths<T> paths = new(graph.VertexCount);

        paths.InitAllRows();

        for (int i = 0; i < graph.VertexCount; i++)
        {
            foreach (var edge in graph.GetOutEdges(i))
            {
                paths.AddPathNode(edge.From, edge.To, edge.From, edge.Weight);
            }
            paths.AddPathNode(i, i, i, T.Zero);
        }

        for (int i = 0; i < graph.VertexCount; i++)
        {
            for (int v1 = 0; v1 < graph.VertexCount; v1++)
            {
                for (int v2 = 0; v2 < graph.VertexCount; v2++)
                {
                    if (paths.IsReachable(v1, i) && paths.IsReachable(i, v2))
                    {
                        if (!paths.IsReachable(v1, v2) || paths.GetDistance(v1, v2) > paths.GetDistance(v1, i) + paths.GetDistance(i, v2))
                        {
                            paths.AddPathNode(v1, v2, paths.GetPrevious(i, v2), paths.GetDistance(v1, i) + paths.GetDistance(i, v2));
                        }
                    }
                }
            }
        }

        for (int i = 0; i < graph.VertexCount; i++)
        {
            for (int v1 = 0; v1 < graph.VertexCount; v1++)
            {
                for (int v2 = 0; v2 < graph.VertexCount; v2++)
                {
                    if (paths.IsReachable(v1, i) && paths.IsReachable(i, v2))
                    {
                        if (!paths.IsReachable(v1, v2) || paths.GetDistance(v1, v2) > paths.GetDistance(v1, i) + paths.GetDistance(i, v2))
                        {
                            throw new InfiniteCycleException();
                        }
                    }
                }
            }
        }

        return paths;
    }
}