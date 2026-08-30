namespace AVGA.GraphLibrary.Tests;

public class MatrixGraphRepresentationTests
{
    [Fact]
    public void AddEdge_IncreasesEdgeCountValue()
    {
        var graph = new MatrixGraphRepresentation<bool>(2);

        Assert.True(graph.EdgeCount.Equals(0));
        Assert.True(graph.AddEdge(0, 1, true));
        Assert.True(graph.EdgeCount.Equals(1));
        Assert.True(graph.AddEdge(1, 0, true));
        Assert.True(graph.EdgeCount.Equals(2));
    }

    [Fact]
    public void AddEdge_FalseAfterAddingExistingEdge()
    {
        var graph = new MatrixGraphRepresentation<bool>(2);

        Assert.True(graph.AddEdge(0, 1, true));
        Assert.False(graph.AddEdge(0, 1, true));
    }

    [Fact]
    public void AddEdge_ThrowsExceptionAfterAddingWrongEdge()
    {
        var graph = new MatrixGraphRepresentation<bool>(2);

        Assert.Throws<InvalidEdgeException>(() => graph.AddEdge(0, -1, true));
        Assert.Throws<InvalidEdgeException>(() => graph.AddEdge(3, 1, true));
    }

    [Fact]
    public void RemoveEdge_DecreasesEdgeCountValue()
    {
        var graph = new MatrixGraphRepresentation<bool>(2);

        Assert.True(graph.EdgeCount.Equals(0));
        Assert.True(graph.AddEdge(0, 1, true));
        Assert.True(graph.EdgeCount.Equals(1));
        Assert.True(graph.RemoveEdge(0, 1));
        Assert.True(graph.EdgeCount.Equals(0));
    }

    [Fact]
    public void AddEdge_FalseAfterRemovingNonExistingEdge()
    {
        var graph = new MatrixGraphRepresentation<bool>(2);

        Assert.False(graph.RemoveEdge(0, 1));
    }

    [Fact]
    public void AddEdge_ThrowsExceptionAfterRemovingWrongEdge()
    {
        var graph = new MatrixGraphRepresentation<bool>(2);

        Assert.Throws<InvalidEdgeException>(() => graph.RemoveEdge(0, -1));
        Assert.Throws<InvalidEdgeException>(() => graph.RemoveEdge(3, 1));
    }

    [Fact]
    public void GetEdgeWeight_GetsExistingEdgeWeight()
    {
        var graph1 = new MatrixGraphRepresentation<bool>(2);
        var graph2 = new MatrixGraphRepresentation<int>(2);

        Assert.True(graph1.AddEdge(0, 1, true));
        Assert.True(graph2.AddEdge(0, 1, 2));

        Assert.True(graph1.GetEdgeWeight(0, 1).Equals(true));
        Assert.True(graph2.GetEdgeWeight(0, 1).Equals(2));
    }

    [Fact]
    public void GetEdgeWeight_ThrowExceptionFromNonExistingEdgeWeight()
    {
        var graph1 = new MatrixGraphRepresentation<bool>(2);
        var graph2 = new MatrixGraphRepresentation<int>(2);

        Assert.Throws<NonExistingEdgeException>(() => graph1.GetEdgeWeight(0, 1));
        Assert.Throws<NonExistingEdgeException>(() => graph2.GetEdgeWeight(0, 1));
    }

    [Fact]
    public void GetNeighbours_ReturnsCorrectCollection()
    {
        var graph = new MatrixGraphRepresentation<bool>(3);

        Assert.True(graph.AddEdge(0, 1, true));
        Assert.True(graph.AddEdge(0, 2, true));

        var neighbours = graph.GetNeighbours(0).ToHashSet();

        Assert.Contains(1, neighbours);
        Assert.Contains(2, neighbours);
    }

    [Fact]
    public void GetOutEdges_ReturnsCorrectCollection()
    {
        var graph = new MatrixGraphRepresentation<int>(3);

        Assert.True(graph.AddEdge(0, 1, 3));
        Assert.True(graph.AddEdge(0, 2, 4));

        var edges = graph.GetOutEdges(0).ToHashSet();

        Assert.Contains((1, 3), edges);
        Assert.Contains((2, 4), edges);
    }

    [Fact]
    public void GetInDegree_ReturnsCorrectValue()
    {
        var graph = new ListGraphRepresentation<int>(3);

        Assert.True(graph.AddEdge(0, 1, 2));
        Assert.True(graph.AddEdge(2, 1, 2));

        Assert.True(graph.GetInDegree(1).Equals(2));
    }

    [Fact]
    public void GetOutDegree_ReturnsCorrectValue()
    {
        var graph = new ListGraphRepresentation<int>(3);

        Assert.True(graph.AddEdge(0, 1, 2));
        Assert.True(graph.AddEdge(0, 2, 2));

        Assert.True(graph.GetOutDegree(0).Equals(2));
    }

    [Fact]
    public void SetEdgeWeight_ExistingEdgesWeightIsChanged()
    {
        var graph = new MatrixGraphRepresentation<int>(2);

        Assert.True(graph.AddEdge(0, 1, 2));

        graph.SetEdgeWeight(0, 1, 3);

        Assert.True(graph.GetEdgeWeight(0, 1).Equals(3));
    }

    [Fact]
    public void SetEdgeWeight_ThrowExceptionAfterChangingNonExistingEdge()
    {
        var graph = new MatrixGraphRepresentation<int>(2);

        Assert.True(graph.AddEdge(0, 1, 2));

        Assert.Throws<InvalidEdgeException>(() => graph.SetEdgeWeight(0, 2, 3));
        Assert.Throws<NonExistingEdgeException>(() => graph.SetEdgeWeight(1, 0, 3));
    }

    [Fact]
    public void HasEdge_CheckingEdgesCorrectly()
    {
        var graph = new MatrixGraphRepresentation<int>(2);

        Assert.True(graph.AddEdge(0, 1, 2));

        Assert.True(graph.HasEdge(0, 1));
        Assert.False(graph.HasEdge(1, 0));
    }

    [Fact]
    public void HasEdge_ThrowExceptionAfterInvalidEdge()
    {
        var graph = new MatrixGraphRepresentation<int>(2);

        Assert.Throws<InvalidEdgeException>(() => graph.SetEdgeWeight(0, 2, 3));
    }
}
