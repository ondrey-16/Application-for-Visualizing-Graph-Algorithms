namespace AVGA.GraphLibrary;

public class InvalidRepresentationTypeException: Exception
{
    public InvalidRepresentationTypeException() 
        : base($"Invalid type of graph's representation has been provided.") 
    {}
}