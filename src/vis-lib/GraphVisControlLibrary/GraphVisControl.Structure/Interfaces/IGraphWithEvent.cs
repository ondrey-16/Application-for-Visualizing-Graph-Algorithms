namespace AVGA.GraphVisControlLibrary;

public interface IGraphWithEvent
{
    public event EventHandler<IGraphEvent>? GraphEvent;
}
