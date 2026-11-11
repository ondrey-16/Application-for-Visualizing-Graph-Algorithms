namespace AVGA.GraphVisControlLibrary;

public record EdgeTraversedEvent : IGraphEvent
{
    public int U { get; private set; }
    public int V { get; private set; }
    public EdgeTraversedEvent(int u, int v)
    {
        U = u;
        V = v;
    }
    public void Execute()
    {
        // Connects with GraphVisualizer
    }
}