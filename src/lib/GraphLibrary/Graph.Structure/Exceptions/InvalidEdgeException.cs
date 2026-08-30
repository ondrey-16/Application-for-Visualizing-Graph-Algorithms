namespace AVGA.GraphLibrary;

public class InvalidEdgeException: Exception
{
    public InvalidEdgeException(int u, int v, int V) 
        : base($"Parameters {u} and {v} representing an edge must be in range of [0, {V}).") 
    {}
}