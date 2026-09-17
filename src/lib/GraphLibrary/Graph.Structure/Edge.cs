namespace AVGA.GraphLibrary;

public struct Edge
{
    public int From { get; }
    public int To { get; }

    public Edge(int from, int to)
    {
        From = from;
        To = to;
    }
}

public struct Edge<T> where T : INumber<T>
{
    public int From { get; }
    public int To { get; }
    public T Weight { get; }

    public Edge(int from, int to, T weight)
    {
        From = from;
        To = to;
        Weight = weight;
    }
}