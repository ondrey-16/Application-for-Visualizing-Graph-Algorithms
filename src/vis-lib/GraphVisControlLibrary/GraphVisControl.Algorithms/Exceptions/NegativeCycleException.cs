namespace AVGA.GraphVisControlLibrary;

public class NegativeCycleException : Exception
{
    public NegativeCycleException()
        : base($"An negative cycle detected.")
    {}
}