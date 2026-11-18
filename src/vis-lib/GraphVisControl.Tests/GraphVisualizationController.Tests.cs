namespace AVGA.GraphVisControl.Tests;

public class GraphVisualizationControllerTests
{
    private sealed class FakeEvent : IGraphEvent
    {
        public int ExecuteCount { get; private set; }

        public void Execute()
        {
            ExecuteCount++;
        }
    }

    private static List<IGraphEvent> CreateEvents(int count)
    {
        var events = new List<IGraphEvent>();

        for (int i = 0; i < count; i++)
        {
            events.Add(new FakeEvent());
        }

        return events;
    }

    [Fact]
    public void NewController_StartsAtStepZero()
    {
        var controller = new GraphVisualizationController(CreateEvents(3));

        Assert.Equal(0, controller.CurrentStep);
    }

    [Fact]
    public void NextStep_MovesToNextEvent()
    {
        var controller = new GraphVisualizationController(CreateEvents(3));

        controller.NextStep();

        Assert.Equal(1, controller.CurrentStep);
    }

    [Fact]
    public void NextStep_AtLastEvent_StaysOnLastEvent()
    {
        var controller = new GraphVisualizationController(CreateEvents(2));

        controller.NextStep();
        controller.NextStep();
        controller.NextStep();

        Assert.Equal(1, controller.CurrentStep);
    }

    [Fact]
    public void NextStep_WithNoEvents_StaysAtZero()
    {
        var controller = new GraphVisualizationController(CreateEvents(0));

        controller.NextStep();

        Assert.Equal(0, controller.CurrentStep);
    }

    [Fact]
    public void PreviousStep_MovesToPreviousEvent()
    {
        var controller = new GraphVisualizationController(CreateEvents(3));
        controller.NextStep();
        controller.NextStep();

        controller.PreviousStep();

        Assert.Equal(1, controller.CurrentStep);
    }

    [Fact]
    public void PreviousStep_AtFirstEvent_StaysAtZero()
    {
        var controller = new GraphVisualizationController(CreateEvents(3));

        controller.PreviousStep();

        Assert.Equal(0, controller.CurrentStep);
    }

    [Fact]
    public void Restart_GoesBackToFirstStep_AndKeepsEvents()
    {
        var events = CreateEvents(3);
        var controller = new GraphVisualizationController(events);
        controller.NextStep();
        controller.NextStep();

        controller.Restart();

        Assert.Equal(0, controller.CurrentStep);
        Assert.Equal(3, events.Count);
    }

    [Fact]
    public void Reset_ClearsEventsAndGoesBackToFirstStep()
    {
        var events = CreateEvents(3);
        var controller = new GraphVisualizationController(events);
        controller.NextStep();

        controller.Reset();

        Assert.Equal(0, controller.CurrentStep);
        Assert.Empty(events);
    }

    [Fact]
    public void ExecuteEvent_ExecutesOnlyTheCurrentEvent()
    {
        var first = new FakeEvent();
        var second = new FakeEvent();
        var controller = new GraphVisualizationController(new List<IGraphEvent> { first, second });
        controller.NextStep();

        controller.ExecuteEvent();

        Assert.Equal(0, first.ExecuteCount);
        Assert.Equal(1, second.ExecuteCount);
    }

    [Fact]
    public void ExecuteEvent_WithNoEvents_Throws()
    {
        var controller = new GraphVisualizationController(CreateEvents(0));

        Assert.Throws<ArgumentOutOfRangeException>(() => controller.ExecuteEvent());
    }
}
