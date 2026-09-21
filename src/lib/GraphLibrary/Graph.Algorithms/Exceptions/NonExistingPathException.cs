namespace AVGA.GraphLibrary;

public class NonExistingPathException : Exception
{
    public NonExistingPathException(int from, int to)
        : base($"Path from {from} to {to} does not exist.")
    {}
}