namespace AVGA.GraphLibrary;

/// <summary>
/// Extends graph classes with DFS algorithms methods.
/// </summary>
public static class DFSGraphExtender
{
    /// <summary>
    /// Returns an object of graph searcher with a stack structure to invoke DFS search methods.
    /// </summary>
    /// <param name="graph">A graph to search.</param>
    public static GraphSearcher DFS(this GraphBase graph) 
        => new GraphSearcher(new Stack<int>(), graph);
    
    /// <summary>
    /// Returns an object of graph searcher with a stack structure to invoke DFS search methods.
    /// </summary>
    /// <param name="graph">A weighted graph to search.</param>
    public static GraphSearcher<T> DFS<T>(this GraphBase<T> graph) where T : INumber<T>, IMinMaxValue<T>
        => new GraphSearcher<T>(new Stack<int>(), graph);
}