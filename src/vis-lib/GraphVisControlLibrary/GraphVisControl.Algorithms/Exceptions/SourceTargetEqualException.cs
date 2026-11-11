namespace AVGA.GraphVisControlLibrary;

public class SourceTargetEqualException : Exception
{
    public SourceTargetEqualException()
        : base($"Source and target are equal")
    {}
}