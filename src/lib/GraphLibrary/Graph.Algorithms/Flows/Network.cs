namespace AVGA.GraphLibrary;

/// <summary>
/// Class represents a flow network (capacities must be positive).
/// </summary>
/// <typeparam name="T">Type of capacities. Must implement INumber interface</typeparam>
public class Network<T> : DirectedGraph<T> where T : INumber<T>
{
    /// <summary>
    /// A graph storing a flow function found by provided algorithms.
    /// </summary>
    public DirectedGraph<T> Flow { get; private set; }

    public Network(int V) : base(V)
    {
        Flow = new(V);
    }

    public Network(Stream s) : base(s)
    {
        Flow = new(VertexCount);

        for (int i = 0; i < VertexCount; i++)
        {
            foreach (var edge in GetOutEdges(i))
            {
                if (Flow.HasEdge(edge.From, edge.To))
                {
                    throw new ReversedEdgeException(edge.From, edge.To);
                }
                if (edge.Weight <= T.Zero)
                {
                    throw new NegativeEdgeWeightException();
                }

                Flow.AddEdge(edge.From, edge.To, T.Zero);
                Flow.AddEdge(edge.To, edge.From, T.Zero);
            }
        }
    }

    public override bool AddEdge(int u, int v, T w)
    {
        return base.AddEdge(u, v, w) && Flow.AddEdge(u, v, T.Zero);
    }
    public override bool RemoveEdge(int u, int v)
    {
        return base.RemoveEdge(u, v) && Flow.RemoveEdge(u, v);
    }

    public override object Clone()
    {
        Network<T> cloned = new(this.VertexCount);
        cloned._representation = (DictionaryRepresentation<T>)this._representation.Clone();
        cloned.Flow = (DirectedGraph<T>)this.Flow.Clone();

        return cloned;
    }
}