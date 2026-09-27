namespace AVGA.GraphLibrary;

internal static class PathValidator
{
    internal static void AreCoordinatesValid(int from, int to, int V)
    {
        if (from < 0 || from >= V || to < 0 | to >= V)
        {
            throw new InvalidPathCoordinatesException(from, to, V);
        }
    }
}