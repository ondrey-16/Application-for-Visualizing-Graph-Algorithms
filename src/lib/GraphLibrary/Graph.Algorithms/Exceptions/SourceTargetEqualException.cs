namespace AVGA.GraphLibrary;

public class SourceTargetEqualException : Exception
{
    public SourceTargetEqualException()
        : base($"Source and target are equal")
    {}
}