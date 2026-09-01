namespace AVGA.GraphLibrary;

public class NonExistingEdgeException: Exception
{
    public NonExistingEdgeException(int u, int v) 
        : base($"Edge ({u}, {v}) does not exist.") 
    {}
}