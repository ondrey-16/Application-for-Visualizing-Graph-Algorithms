namespace AVGA.GraphLibrary;

public class InvalidVertexException: Exception
{
    public InvalidVertexException(int v, int V) 
        : base($"Parameter {v} representing a number of vertex must be in range of [0, {V}).") 
    {}
}