namespace AVGA.GraphLibrary;

public class InvalidPathCoordinatesException : ArgumentException
{
    public InvalidPathCoordinatesException(int from, int to, int V)
        : base($"Your path coordinates: ({from}, {to}) are out of range. Both of vertices must be in a range [0, {V}).")
    {}
}