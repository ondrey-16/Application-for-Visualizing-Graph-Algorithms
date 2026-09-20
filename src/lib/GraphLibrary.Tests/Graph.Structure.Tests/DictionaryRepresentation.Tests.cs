namespace AVGA.GraphLibrary.Tests;

public class DictionaryRepresentationTests
{
    [Fact]
    public void AddEdge_IncreasesEdgeCountValue()
    {
        var graph = new DictionaryRepresentation<int>(2);

        Assert.True(graph.EdgeCount.Equals(0));
        Assert.True(graph.AddEdge(0, 1));
        Assert.True(graph.EdgeCount.Equals(1));
        Assert.True(graph.AddEdge(1, 0));
        Assert.True(graph.EdgeCount.Equals(2));
    }

    [Fact]
    public void AddEdge_FalseAfterAddingExistingEdge()
    {
        var graph = new DictionaryRepresentation<int>(2);

        Assert.True(graph.AddEdge(0, 1));
        Assert.False(graph.AddEdge(0, 1));
    }

    [Fact]
    public void RemoveEdge_DecreasesEdgeCountValue()
    {
        var graph = new DictionaryRepresentation<int>(2);

        Assert.True(graph.EdgeCount.Equals(0));
        Assert.True(graph.AddEdge(0, 1));
        Assert.True(graph.EdgeCount.Equals(1));
        Assert.True(graph.RemoveEdge(0, 1));
        Assert.True(graph.EdgeCount.Equals(0));
    }

    [Fact]
    public void AddEdge_FalseAfterRemovingNonExistingEdge()
    {
        var graph = new DictionaryRepresentation<int>(2);

        Assert.False(graph.RemoveEdge(0, 1));
    }

    [Fact]
    public void GetEdgeWeight_GetsExistingEdgeWeight()
    {
        var graph1 = new DictionaryRepresentation<int>(2);
        var graph2 = new DictionaryRepresentation<float>(2);

        Assert.True(graph1.AddEdge(0, 1));
        Assert.True(graph2.AddEdge(0, 1));

        Assert.True(graph1.GetEdgeWeight(0, 1).Equals(0));
        Assert.True(graph2.GetEdgeWeight(0, 1).Equals(0.0f));
    }

    [Fact]
    public void GetEdgeWeight_ThrowExceptionFromNonExistingEdgeWeight()
    {
        var graph1 = new DictionaryRepresentation<int>(2);
        var graph2 = new DictionaryRepresentation<float>(2);

        Assert.Throws<NonExistingEdgeException>(() => graph1.GetEdgeWeight(0, 1));
        Assert.Throws<NonExistingEdgeException>(() => graph2.GetEdgeWeight(0, 1));
    }

    [Fact]
    public void GetNeighbours_ReturnsCorrectCollection()
    {
        var graph = new DictionaryRepresentation<int>(3);

        Assert.True(graph.AddEdge(0, 1));
        Assert.True(graph.AddEdge(0, 2));

        var neighbours = graph.GetNeighbours(0).ToHashSet();

        Assert.Contains(1, neighbours);
        Assert.Contains(2, neighbours);
    }

    [Fact]
    public void GetOutEdges_ReturnsCorrectCollection()
    {
        var graph = new DictionaryRepresentation<int>(3);

        Assert.True(graph.AddEdge(0, 1));
        graph.SetEdgeWeight(0, 1, 3);
        Assert.True(graph.AddEdge(0, 2));
        graph.SetEdgeWeight(0, 2, 4);

        var edges = graph.GetOutEdges(0);

        Assert.Contains(new Edge<int>(0, 1, 3), edges);
        Assert.Contains(new Edge<int>(0, 2, 4), edges);
    }

    [Fact]
    public void GetInDegree_ReturnsCorrectValue()
    {
        var graph = new DictionaryRepresentation<int>(3);

        Assert.True(graph.AddEdge(0, 1));
        Assert.True(graph.AddEdge(2, 1));

        Assert.True(graph.GetInDegree(1).Equals(2));
    }

    [Fact]
    public void GetOutDegree_ReturnsCorrectValue()
    {
        var graph = new DictionaryRepresentation<int>(3);

        Assert.True(graph.AddEdge(0, 1));
        Assert.True(graph.AddEdge(0, 2));

        Assert.True(graph.GetOutDegree(0).Equals(2));
    }

    [Fact]
    public void SetEdgeWeight_ExistingEdgesWeightIsChanged()
    {
        var graph = new DictionaryRepresentation<int>(2);

        Assert.True(graph.AddEdge(0, 1));

        graph.SetEdgeWeight(0, 1, 3);

        Assert.True(graph.GetEdgeWeight(0, 1).Equals(3));
    }

    [Fact]
    public void SetEdgeWeight_ThrowExceptionAfterChangingNonExistingEdge()
    {
        var graph = new DictionaryRepresentation<int>(2);

        Assert.True(graph.AddEdge(0, 1));

        Assert.Throws<InvalidEdgeException>(() => graph.SetEdgeWeight(0, 2, 3));
        Assert.Throws<NonExistingEdgeException>(() => graph.SetEdgeWeight(1, 0, 3));
    }

    [Fact]
    public void HasEdge_CheckingEdgesCorrectly()
    {
        var graph = new DictionaryRepresentation<int>(2);

        Assert.True(graph.AddEdge(0, 1));

        Assert.True(graph.HasEdge(0, 1));
        Assert.False(graph.HasEdge(1, 0));
    }

    [Fact]
    public void HasEdge_ThrowExceptionAfterInvalidEdge()
    {
        var graph = new DictionaryRepresentation<int>(2);

        Assert.Throws<InvalidEdgeException>(() => graph.SetEdgeWeight(0, 2, 3));
    }

    [Fact]
    public void ReadingStream_ValidStructure()
    {
        string s = """
        0 1 3
        1 2 2
        2 0 1
        """;

        MemoryStream ms = new();
        StreamWriter sw = new(ms);
        sw.Write(s);
        sw.Flush();
        ms.Position = 0;

        var graph = new DictionaryRepresentation<int>(ms);

        Assert.True(graph.VertexCount == 3);
        Assert.True(graph.EdgeCount == 3);
        Assert.True(graph.GetEdgeWeight(0, 1) == 3);
        Assert.True(graph.GetEdgeWeight(1, 2) == 2);
        Assert.True(graph.GetEdgeWeight(2, 0) == 1);
    }
}
