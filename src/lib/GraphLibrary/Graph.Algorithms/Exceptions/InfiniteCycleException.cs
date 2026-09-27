namespace AVGA.GraphLibrary;

public class InfiniteCycleException : Exception
{
    public InfiniteCycleException()
        : base($"Graph structure error! - An infinite cycle has been detected.")
    {}
}