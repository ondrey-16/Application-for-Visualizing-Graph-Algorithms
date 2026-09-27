namespace AVGA.GraphLibrary;

public static class BFSGraphExtender
{
    /// <summary>
    /// Returns an object of graph searcher with a queue structure to invoke BFS search methods.
    /// </summary>
    /// <param name="graph">A graph to search.</param>
    public static GraphSearcher BFS(this GraphBase graph) 
        => new GraphSearcher(new Queue<int>(), graph);
    
    /// <summary>
    /// Returns an object of graph searcher with a queue structure to invoke BFS search methods.
    /// </summary>
    /// <param name="graph">A weighted graph to search.</param>
    public static GraphSearcher<T> BFS<T>(this GraphBase<T> graph) where T : INumber<T>, IMinMaxValue<T>
        => new GraphSearcher<T>(new Queue<int>(), graph);
}