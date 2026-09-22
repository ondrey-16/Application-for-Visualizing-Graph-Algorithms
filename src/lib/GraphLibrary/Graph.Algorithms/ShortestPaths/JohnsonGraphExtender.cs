namespace AVGA.GraphLibrary;

public static class JohnsonGraphExtender
{
    public static Paths<T> Johnson<T>(this GraphBase<T> graph) where T : INumber<T>, IMinMaxValue<T>
    {
        DirectedGraph<T> tmpGraph = new(graph.VertexCount + 1);
        DirectedGraph<T> newWeightedGraph = new(graph.VertexCount);
        for (int i = 0; i < graph.VertexCount; i++)
        {
            foreach (var edge in graph.GetOutEdges(i))
            {
                tmpGraph.AddEdge(edge.From, edge.To);
                tmpGraph.SetEdgeWeight(edge.From, edge.To, edge.Weight);
                newWeightedGraph.AddEdge(edge.From, edge.To);
                newWeightedGraph.SetEdgeWeight(edge.From, edge.To, edge.Weight);
            }
            tmpGraph.AddEdge(graph.VertexCount, i);
        }

        Paths<T> tmpPaths = tmpGraph.BellmanFord(graph.VertexCount);

        for (int i = 0; i < graph.VertexCount; i++)
        {
            foreach (var edge in graph.GetOutEdges(i))
            {
                tmpGraph.SetEdgeWeight(edge.From, edge.To, 
                    edge.Weight + tmpPaths.GetDistance(graph.VertexCount, edge.From) - tmpPaths.GetDistance(graph.VertexCount, edge.To));
            }
        }

        for (int i = 0; i < graph.VertexCount; i++)
        {
            foreach (var edge in graph.GetOutEdges(i))
            {
                newWeightedGraph.SetEdgeWeight(edge.From, edge.To, 
                    edge.Weight + tmpPaths.GetDistance(graph.VertexCount, edge.From) - tmpPaths.GetDistance(graph.VertexCount, edge.To));
            }
        }

        Paths<T> paths = new(newWeightedGraph.VertexCount);
        for (int i = 0; i < newWeightedGraph.VertexCount; i++)
        {
            paths = newWeightedGraph.Dijkstra(i, paths);
        }

        return paths;
    }
}