namespace AVGA.GraphLibrary.Tests;

public class DirectedGraphTests
{
    [Fact]
    public void ReadingStream_ForUnweightedDirectedGraph_ValidStructure_AndChangingRepresentation()
    {
        string s = """
        0 1
        1 2
        2 0
        """;

        MemoryStream ms = new();
        StreamWriter sw = new(ms);
        sw.Write(s);
        sw.Flush();
        ms.Position = 0;

        var graph = new DirectedGraph(ms);

        Assert.True(graph.VertexCount == 3);
        Assert.True(graph.EdgeCount == 3);

        var cloned = (DirectedGraph) graph.Clone();

        Assert.True(cloned.HasEdge(0, 1));
        Assert.True(cloned.HasEdge(1, 2));
        Assert.True(cloned.HasEdge(2, 0));
    }

    [Fact]
    public void ReadingStream_ForWeightedDirectedGraph_ValidStructure_AndChangingRepresentation()
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

        var graph = new DirectedGraph<int>(ms);

        Assert.True(graph.VertexCount == 3);
        Assert.True(graph.EdgeCount == 3);
        Assert.True(graph.GetEdgeWeight(0, 1) == 3);
        Assert.True(graph.GetEdgeWeight(1, 2) == 2);
        Assert.True(graph.GetEdgeWeight(2, 0) == 1);

        var cloned = (DirectedGraph<int>) graph.Clone();

        Assert.True(cloned.GetEdgeWeight(0, 1) == 3);
        Assert.True(cloned.GetEdgeWeight(1, 2) == 2);
        Assert.True(cloned.GetEdgeWeight(2, 0) == 1);
    }
}