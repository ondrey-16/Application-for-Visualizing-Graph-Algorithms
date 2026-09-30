namespace AVGA.GraphLibrary;

public static class FordFulkersonNetworkExtender
{
    /// <summary>
    /// Modifies provided network object's flow graph finding maximum possible flow using Ford-Fulkerson method.
    /// </summary>
    /// <typeparam name="T">Type of capacities.</typeparam>
    /// <param name="network">Flow network.</param>
    /// <param name="source">Network source vertex.</param>
    /// <param name="target">Network target vertex.</param>
    /// <returns>Value of found maximum flow</returns>
    /// <exception cref="SourceTargetEqualException">Thrown if source and target vertices are equal.</exception>
    /// <exception cref="NegativeEdgeWeightException">Thrown if an edge with negative capacity has been detected.</exception>
    public static T FordFulkerson<T>(this Network<T> network, int source, int target) where T : INumber<T>
    {
        if (source == target)
        {
            throw new SourceTargetEqualException();
        }

        DirectedGraph<T> residual = new(network.VertexCount);

        for (int i = 0; i < network.VertexCount; i++)
        {
            foreach (var edge in network.GetOutEdges(i))
            {
                T c = network.GetEdgeWeight(edge.From, edge.To);
                if (c <= T.Zero)
                {
                    throw new NegativeEdgeWeightException();
                }
                residual.AddEdge(edge.From, edge.To, c);
            }
        }

        return modifyFlowFromResidual(source, target, residual, network.Flow, network);
    }

    /// <summary>
    /// Modifies provided network object's flow graph with costs finding maximum possible flow using Ford-Fulkerson method.
    /// </summary>
    /// <typeparam name="T">Type of capacities.</typeparam>
    /// <param name="network">Flow network.</param>
    /// <param name="source">Network source vertex.</param>
    /// <param name="target">Network target vertex.</param>
    /// <returns>Value of found maximum flow</returns>
    /// <exception cref="SourceTargetEqualException">Thrown if source and target vertices are equal.</exception>
    /// <exception cref="NegativeEdgeWeightException">Thrown if an edge with negative capacity has been detected.</exception>
    public static TCapacity FordFulkerson<TCapacity, TCost>(this CostNetwork<TCapacity, TCost> 
        network, int source, int target) where TCapacity : INumber<TCapacity> where TCost : INumber<TCost>
    {
        if (source == target)
        {
            throw new SourceTargetEqualException();
        }

        DirectedGraph<TCapacity> residual = new(network.VertexCount);
        DirectedGraph<TCapacity> tmpNetwork = new(network.VertexCount);

        for (int i = 0; i < network.VertexCount; i++)
        {
            foreach (var edge in network.GetOutEdges(i))
            {
                TCapacity c = network.GetEdgeWeight(edge.From, edge.To).Capacity;
                if (c <= TCapacity.Zero)
                {
                    throw new NegativeEdgeWeightException();
                }
                residual.AddEdge(edge.From, edge.To, c);
                tmpNetwork.AddEdge(edge.From, edge.To, c);
            }
        }

        return modifyFlowFromResidual(source, target, residual, network.Flow, tmpNetwork);
    }

    /// <summary>
    /// Helper function for both Ford-Fulkerson methods versions which modifies network's flow graph and finds maximum flow value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source">Network source vertex.</param>
    /// <param name="target">Network target vertex.</param>
    /// <param name="residual">Residual network graph.</param>
    /// <param name="flow">Flow graph.</param>
    /// <param name="network">Flow network graph.</param>
    /// <returns>Value of found maximum flow</returns>
    private static T modifyFlowFromResidual<T>(int source, int target, DirectedGraph<T> residual, 
        DirectedGraph<T> flow, DirectedGraph<T> network) where T : INumber<T>
    {
        T maxFlow = T.Zero;
        List<int>? path;
        while ((path = residual.GetPathDFS(source, target)) is not null)
        {
            T cmin = residual.GetEdgeWeight(path[0], path[1]);
            for (int i = 1; i < path.Count - 1; i++)
            {
                T c = residual.GetEdgeWeight(path[i], path[i + 1]);
                if (c < cmin)
                {
                    cmin = c;
                }
            }
            maxFlow += cmin;
            for (int i = 0; i < path.Count - 1; i++)
            {
                int from = path[i];
                int to = path[i + 1];
                T currf;
                if (network.HasEdge(path[i], path[i + 1]))
                {
                    currf = flow.GetEdgeWeight(from, to);
                    flow.SetEdgeWeight(from, to, currf + cmin);
                }
                else
                {
                    currf = flow.GetEdgeWeight(to, from);
                    flow.SetEdgeWeight(to, from, currf - cmin);
                }
                
                T front = residual.GetEdgeWeight(from, to) - cmin;
                if (front == T.Zero)
                {
                    residual.RemoveEdge(from, to);
                }
                else
                {
                    residual.SetEdgeWeight(from, to, front);
                }

                if (!residual.HasEdge(to, from))
                {
                    residual.AddEdge(to, from, cmin);
                }
                else
                {
                    T back = residual.GetEdgeWeight(to, from) + cmin;
                    residual.SetEdgeWeight(to, from, back);
                }
            }
        }

        return maxFlow;
    }
}