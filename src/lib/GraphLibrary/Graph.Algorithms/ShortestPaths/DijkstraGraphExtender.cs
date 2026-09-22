namespace AVGA.GraphLibrary;

public static class DijkstraGraphExtender
{
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