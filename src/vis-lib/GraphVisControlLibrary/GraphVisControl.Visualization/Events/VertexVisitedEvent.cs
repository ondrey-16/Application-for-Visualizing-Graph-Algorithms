namespace AVGA.GraphVisControlLibrary;

public record VertexVisitedEvent : IGraphEvent
{
    public int V { get; private set; }
    public VertexVisitedEvent(int v)
    {
        V = v;
    }
    public void Execute()
    {
        // Connects with GraphVisualizer
    }
}