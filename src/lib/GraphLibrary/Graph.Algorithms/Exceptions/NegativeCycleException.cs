namespace AVGA.GraphLibrary;

public class NegativeCycleException : Exception
{
    public NegativeCycleException()
        : base($"An infinite cycle detected.")
    {}
}