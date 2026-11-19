namespace AVGA.GraphVisControlLibrary;

public class GraphVisualizationController
{
    private readonly List<IGraphEvent> _graphEvents;
    private Action? _unsubscribe;
    public int CurrentStep { get; private set; }
    public int EventsCount => _graphEvents.Count;

    public GraphVisualizationController(IGraphWithEvent graph)
    {
        _graphEvents = new List<IGraphEvent>();
        CurrentStep = 0;
        graph.GraphEvent += GraphEventHandler;
        _unsubscribe = () => graph.GraphEvent -= GraphEventHandler;
    }

    public void Subscribe(IGraphWithEvent graph)
    {
        Unsubscribe();
        graph.GraphEvent += GraphEventHandler;
        _unsubscribe = () => graph.GraphEvent -= GraphEventHandler;
    }

    private void GraphEventHandler(object? sender, IGraphEvent graphEvent)
    {
        _graphEvents.Add(graphEvent);
    }

    private void Unsubscribe()
    {
        _unsubscribe?.Invoke();
        _unsubscribe = null;
        _graphEvents.Clear();
        CurrentStep = 0;
    }

    public void NextStep()
    {
        if (CurrentStep < _graphEvents.Count - 1)
        {
            CurrentStep++;
        }
    }
    public void PreviousStep()
    {
        if (CurrentStep > 0)
        {
            CurrentStep--;
        }
    }
    public void Restart()
    {
        CurrentStep = 0;
    }
    public void Reset()
    {
        Unsubscribe();
    }
    public void ExecuteEvent()
    {
        _graphEvents[CurrentStep].Execute();
    }
}
