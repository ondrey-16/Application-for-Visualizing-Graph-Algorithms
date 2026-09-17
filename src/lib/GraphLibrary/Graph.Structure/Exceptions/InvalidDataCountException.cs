namespace AVGA.GraphLibrary;

public class InvalidDataCountException: Exception
{
    public InvalidDataCountException(int count, int validCount) 
        : base($"Invalid count of separated data from line: {count}. Should be {validCount}.") 
    {}
}