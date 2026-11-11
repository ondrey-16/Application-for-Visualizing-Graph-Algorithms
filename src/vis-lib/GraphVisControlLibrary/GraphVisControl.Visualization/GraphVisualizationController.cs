namespace AVGA.GraphVisControlLibrary;

public class GraphVisualizationController
{
    private List<IGraphEvent> _graphEvents;
    public int CurrentStep { get; private set; }

    public GraphVisualizationController(List<IGraphEvent> graphEvents)
    {
        _graphEvents = graphEvents;
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
        _graphEvents.Clear();
        CurrentStep = 0;
    }
    public void ExecuteEvent()
    {
        _graphEvents[CurrentStep].Execute();
    }
}