namespace AVGA.GraphLibrary;

public static class BellmanFordGraphExtender
{
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