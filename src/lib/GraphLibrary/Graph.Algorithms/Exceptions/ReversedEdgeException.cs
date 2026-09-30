namespace AVGA.GraphLibrary;

public class ReversedEdgeException : Exception
{
    public ReversedEdgeException(int from, int to)
        : base($"A reversed edge ({from}, {to}) detected.")
    {}
}