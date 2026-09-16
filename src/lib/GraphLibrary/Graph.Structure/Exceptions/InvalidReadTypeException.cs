namespace AVGA.GraphLibrary;

public class InvalidReadTypeException: Exception
{
    public InvalidReadTypeException(string s) 
        : base($"Read value: {s} cannot be parsed to wanted data type.") 
    {}
}