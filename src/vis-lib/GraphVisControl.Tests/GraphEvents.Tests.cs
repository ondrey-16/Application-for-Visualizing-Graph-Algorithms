namespace AVGA.GraphVisControl.Tests;

public class GraphEventsTests
{
    [Fact]
    public void VertexVisitedEvent_StoresVertex()
    {
        var e = new VertexVisitedEvent(5);

        Assert.Equal(5, e.V);
    }

    [Fact]
    public void VertexVisitedEvent_WithSameVertex_AreEqual()
    {
        Assert.Equal(new VertexVisitedEvent(2), new VertexVisitedEvent(2));
    }

    [Fact]
    public void VertexVisitedEvent_WithDifferentVertex_AreNotEqual()
    {
        Assert.NotEqual(new VertexVisitedEvent(2), new VertexVisitedEvent(3));
    }

    [Fact]
    public void EdgeTraversedEvent_StoresBothEndpoints()
    {
        var e = new EdgeTraversedEvent(1, 4);

        Assert.Equal(1, e.U);
        Assert.Equal(4, e.V);
    }

    [Fact]
    public void EdgeTraversedEvent_WithSameEndpoints_AreEqual()
    {
        Assert.Equal(new EdgeTraversedEvent(1, 4), new EdgeTraversedEvent(1, 4));
    }

    [Fact]
    public void EdgeTraversedEvent_WithReversedEndpoints_AreNotEqual()
    {
        Assert.NotEqual(new EdgeTraversedEvent(1, 4), new EdgeTraversedEvent(4, 1));
    }
}
