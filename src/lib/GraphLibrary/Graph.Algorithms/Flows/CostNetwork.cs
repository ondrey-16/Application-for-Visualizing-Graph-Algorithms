namespace AVGA.GraphLibrary;

/// <summary>
/// Class represents a flow network with costs of any numeric value (capacities must be positive).
/// </summary>
/// <typeparam name="TCapacity">Type of capacity. Must implement INumber interface.</typeparam>
/// <typeparam name="TCost">Type of cost. Must implement INumber interface.</typeparam>
public class CostNetwork<TCapacity, TCost> : DirectedGraph<(TCapacity Capacity, TCost Cost)> 
    where TCapacity : INumber<TCapacity> 
    where TCost : INumber<TCost>
{
    /// <summary>
    /// A graph storing a flow function found by provided algorithms.
    /// </summary>
    public DirectedGraph<TCapacity> Flow { get; private set; }

    public CostNetwork(int V) : base(V)
    {
        Flow = new(V);
    }

    public CostNetwork(Stream s) : base(s)
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
                if (edge.Weight.Capacity <= TCapacity.Zero)
                {
                    throw new NegativeEdgeWeightException();
                }

                Flow.AddEdge(edge.From, edge.To, TCapacity.Zero);
                Flow.AddEdge(edge.To, edge.From, TCapacity.Zero);
            }
        }
    }

    public override bool AddEdge(int u, int v, (TCapacity, TCost) w)
    {
        return base.AddEdge(u, v, w) && Flow.AddEdge(u, v, TCapacity.Zero);
    }
    public override bool RemoveEdge(int u, int v)
    {
        return base.RemoveEdge(u, v) && Flow.RemoveEdge(u, v);
    }
    public override object Clone()
    {
        CostNetwork<TCapacity, TCost> cloned = new(this.VertexCount);
        cloned._representation = (DictionaryRepresentation<(TCapacity, TCost)>)this._representation.Clone();
        cloned.Flow = (DirectedGraph<TCapacity>)this.Flow.Clone();

        return cloned;
    }

    protected override (TCapacity, TCost) WeightReader(List<string> weightParts)
    {
        if (weightParts.Count != 2)
        {
            throw new InvalidDataCountException(weightParts.Count + 2, 4);
        }

        var converterCapacity = TypeDescriptor.GetConverter(typeof(TCapacity));
        var converterCost = TypeDescriptor.GetConverter(typeof(TCost));

        TCapacity? readCapacity;
        TCost? readCost;

        if (converterCapacity is null || converterCost is null)
        {
            throw new TypeCannotBeReadException<(TCapacity, TCost)>();
        }

        readCapacity = (TCapacity?)converterCapacity.ConvertFromString(weightParts[0]);

        if (readCapacity is null)
        {
            throw new InvalidReadTypeException(weightParts[0]);
        }

        readCost = (TCost?)converterCost.ConvertFromString(weightParts[1]);

        if (readCost is null)
        {
            throw new InvalidReadTypeException(weightParts[1]);
        }

        return (readCapacity, readCost);
    }
}