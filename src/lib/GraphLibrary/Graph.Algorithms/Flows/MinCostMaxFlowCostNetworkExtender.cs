namespace AVGA.GraphLibrary;

public static class MinCostMaxFlowCostNetworkExtender
{
    /// <summary>
    /// Modifies provided network object's flow graph with costs finding maximum possible flow with minimum cost.
    /// </summary>
    /// <typeparam name="TCapacity">Type of capacities.</typeparam>
    /// <typeparam name="TCost">Type of costs.</typeparam>
    /// <param name="network">Flow network.</param>
    /// <param name="source">Network source vertex.</param>
    /// <param name="target">Network target vertex.</param>
    /// <returns></returns>
    /// <exception cref="NegativeEdgeWeightException"></exception>
    public static (TCapacity flow, TCost Cost) MinCostMaxFlow<TCapacity, TCost>(this CostNetwork<TCapacity, TCost> network, int source, int target) 
        where TCapacity : INumber<TCapacity>, IMinMaxValue<TCapacity>
        where TCost : INumber<TCost>, IMinMaxValue<TCost>
    {
        TCapacity maxFlow = network.FordFulkerson(source, target);

        DirectedGraph<TCapacity> residual = new(network.VertexCount);
        DirectedGraph<TCost> residualCost = new(network.VertexCount);

        TCost minCost = TCost.Zero;

        for (int i = 0; i < network.VertexCount; i++)
        {
            foreach (var edge in network.GetOutEdges(i))
            {
                TCapacity c = network.GetEdgeWeight(edge.From, edge.To).Capacity;
                TCapacity f = network.Flow.GetEdgeWeight(edge.From, edge.To);
                TCost cost = network.GetEdgeWeight(edge.From, edge.To).Cost;
                if (c <= TCapacity.Zero)
                {
                    throw new NegativeEdgeWeightException();
                }

                if (c - f > TCapacity.Zero)
                {
                    residual.AddEdge(edge.From, edge.To, c - f);
                    residualCost.AddEdge(edge.From, edge.To, cost);
                }
                if (f > TCapacity.Zero)
                {
                    residual.AddEdge(edge.To, edge.From, f);
                    residualCost.AddEdge(edge.To, edge.From, -cost);
                }

                minCost += cost * TCost.CreateChecked(f);
            }
        }

        List<int>? cycle;

        while ((cycle = residualCost.NegativeCycle()) is not null)
        {
            TCapacity Delta = residual.GetEdgeWeight(cycle[0], cycle[1]);
            TCost cycleCost = residualCost.GetEdgeWeight(cycle[0], cycle[1]);
            for (int i = 1; i < cycle.Count - 1; i++)
            {
                TCapacity currC = residual.GetEdgeWeight(cycle[i], cycle[i + 1]);
                if (currC < Delta)
                {
                    Delta = currC;
                }
                cycleCost += residualCost.GetEdgeWeight(cycle[i], cycle[i + 1]);
            }

            minCost += cycleCost * TCost.CreateChecked(Delta);

            for (int i = 0; i < cycle.Count - 1; i++)
            {
                int from = cycle[i];
                int to = cycle[i + 1];
                TCapacity currC;
                if (network.HasEdge(cycle[i], cycle[i + 1]))
                {
                    currC = network.Flow.GetEdgeWeight(from, to);
                    network.Flow.SetEdgeWeight(from, to, currC + Delta);
                }
                else
                {
                    currC = network.Flow.GetEdgeWeight(to, from);
                    network.Flow.SetEdgeWeight(to, from, currC - Delta);
                }
                
                TCapacity front = residual.GetEdgeWeight(from, to) - Delta;
                if (front == TCapacity.Zero)
                {
                    residual.RemoveEdge(from, to);
                    residualCost.RemoveEdge(from, to);
                }
                else
                {
                    residual.SetEdgeWeight(from, to, front);
                }

                if (front != TCapacity.Zero && !residual.HasEdge(to, from))
                {
                    residual.AddEdge(to, from, Delta);
                    TCost cost = residualCost.GetEdgeWeight(from, to);
                    residualCost.AddEdge(to, from, -cost);
                }
                else
                {
                    TCapacity back = residual.GetEdgeWeight(to, from) + Delta;
                    residual.SetEdgeWeight(to, from, back);
                }
            }
        }

        return (maxFlow, minCost);
    }
}