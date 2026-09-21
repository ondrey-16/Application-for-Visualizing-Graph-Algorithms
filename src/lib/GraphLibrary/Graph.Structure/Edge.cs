namespace AVGA.GraphLibrary;

/// <summary>
/// A structure of graph edge.
/// </summary>
public struct Edge
{
    /// <summary>
    /// A vertex which edge goes out.
    /// </summary>
    public int From { get; }

    /// <summary>
    /// A vertex which edge comes in.
    /// </summary>
    public int To { get; }

    public Edge(int from, int to)
    {
        From = from;
        To = to;
    }
}


/// <summary>
/// A structure of weighted graph edge.
/// </summary>
/// <typeparam name="T">Type of edge weight.</typeparam>
public struct Edge<T> where T : INumber<T>
{
     /// <summary>
    /// A vertex which edge goes out.
    /// </summary>
    public int From { get; }
    
    /// <summary>
    /// A vertex which edge comes in.
    /// </summary>
    public int To { get; }

    /// <summary>
    /// A weight of edge.
    /// </summary>
    public T Weight { get; }

    public Edge(int from, int to, T weight)
    {
        From = from;
        To = to;
        Weight = weight;
    }
}