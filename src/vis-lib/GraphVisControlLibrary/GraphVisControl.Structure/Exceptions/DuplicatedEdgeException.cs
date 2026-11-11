namespace AVGA.GraphVisControlLibrary;

public class DuplicatedEdgeException: Exception
{
    public DuplicatedEdgeException(int u, int v) 
        : base($"Edge ({u}, {v}) already exists in representation.") 
    {}
}