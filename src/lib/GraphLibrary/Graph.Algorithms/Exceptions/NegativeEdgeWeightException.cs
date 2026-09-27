namespace AVGA.GraphLibrary;

public class NegativeEdgeWeightException : Exception
{
    public NegativeEdgeWeightException()
        : base($"An negative edge weight detected.")
    {}
}