namespace AVGA.GraphVisControl.Tests;

public class GraphVisualizationControllerTests
{
    private sealed class FakeVisualizationState
    {
        public int VisualizationState = -1;
    }
    private static readonly FakeVisualizationState _visualizationState = new();
    private sealed class FakeEvent : IGraphEvent
    {
        public int ExecuteNum { get; private set; }
        private readonly FakeVisualizationState _visualizationState;
        public FakeEvent(FakeVisualizationState visualizationState, int executeNum)
        {
            _visualizationState = visualizationState;
            ExecuteNum = executeNum;
        }

        public void Execute()
        {
            _visualizationState.VisualizationState = ExecuteNum;
        }
    }

    private sealed class FakeEventGraph : IGraphWithEvent
    {
        public event EventHandler<IGraphEvent>? GraphEvent;
        public void InvokeFakeEvent(int executeNum)
        {
            GraphEvent?.Invoke(this, new FakeEvent(_visualizationState, executeNum));
        }
    }

    private static void CreateEvents(FakeEventGraph graph, int count)
    {
        for (int i = 0; i < count; i++)
        {
            graph.InvokeFakeEvent(i);
        }
    }

    [Fact]
    public void NewController_StartsAtStepZero()
    {
        FakeEventGraph graph = new();
        var controller = new GraphVisualizationController(graph);

        Assert.Equal(0, controller.CurrentStep);
    }

    [Fact]
    public void NextStep_MovesToNextEvent()
    {
        FakeEventGraph graph = new();
        var controller = new GraphVisualizationController(graph);
        CreateEvents(graph, 3);

        controller.NextStep();

        Assert.Equal(1, controller.CurrentStep);
    }

    [Fact]
    public void NextStep_AtLastEvent_StaysOnLastEvent()
    {
        FakeEventGraph graph = new();
        var controller = new GraphVisualizationController(graph);
        CreateEvents(graph, 3);

        controller.NextStep();
        controller.NextStep();
        controller.NextStep();

        Assert.Equal(2, controller.CurrentStep);
    }

    [Fact]
    public void NextStep_WithNoEvents_StaysAtZero()
    {
        FakeEventGraph graph = new();
        var controller = new GraphVisualizationController(graph);

        controller.NextStep();

        Assert.Equal(0, controller.CurrentStep);
    }

    [Fact]
    public void PreviousStep_MovesToPreviousEvent()
    {
        FakeEventGraph graph = new();
        var controller = new GraphVisualizationController(graph);
        CreateEvents(graph, 3);
        
        controller.NextStep();
        controller.NextStep();

        controller.PreviousStep();

        Assert.Equal(1, controller.CurrentStep);
    }

    [Fact]
    public void PreviousStep_AtFirstEvent_StaysAtZero()
    {
        FakeEventGraph graph = new();
        var controller = new GraphVisualizationController(graph);
        CreateEvents(graph, 3);

        controller.PreviousStep();

        Assert.Equal(0, controller.CurrentStep);
    }

    [Fact]
    public void Restart_GoesBackToFirstStep_AndKeepsEvents()
    {
        FakeEventGraph graph = new();
        var controller = new GraphVisualizationController(graph);
        CreateEvents(graph, 3);

        controller.NextStep();
        controller.NextStep();

        controller.Restart();

        Assert.Equal(0, controller.CurrentStep);
        Assert.Equal(3, controller.EventsCount);
    }

    [Fact]
    public void Reset_ClearsEventsAndGoesBackToFirstStep()
    {
        FakeEventGraph graph = new();
        var controller = new GraphVisualizationController(graph);
        CreateEvents(graph, 3);

        controller.NextStep();

        controller.Reset();

        Assert.Equal(0, controller.CurrentStep);
        Assert.Equal(0, controller.EventsCount);
    }

    [Fact]
    public void ExecuteEvent_ExecutesOnlyTheCurrentEvent()
    {
        FakeEventGraph graph = new();
        var controller = new GraphVisualizationController(graph);
        CreateEvents(graph, 3);

        controller.NextStep();
        controller.ExecuteEvent();

        Assert.Equal(1, _visualizationState.VisualizationState);

        controller.NextStep();
        controller.ExecuteEvent();

        Assert.Equal(2, _visualizationState.VisualizationState);
    }

    [Fact]
    public void ExecuteEvent_WithNoEvents_Throws()
    {
        FakeEventGraph graph = new();
        var controller = new GraphVisualizationController(graph);

        Assert.Throws<ArgumentOutOfRangeException>(() => controller.ExecuteEvent());
    }
}
