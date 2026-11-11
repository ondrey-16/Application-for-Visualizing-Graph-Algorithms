namespace AVGA.GraphVisControlLibrary;

public class NegativeEdgeWeightException : Exception
{
    public NegativeEdgeWeightException()
        : base($"An negative edge weight detected.")
    {}
}