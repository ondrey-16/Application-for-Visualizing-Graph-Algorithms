namespace AVGA.GraphLibrary.Tests;

public class UnweightedGraphTests
{
    [Fact]
    public void ReadingStream_DirectedGraph_ValidStructure()
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

        var graph = new UnweightedGraph(ms, RepresentationTypeEnum.LIST, true);

        Assert.True(graph.VertexCount == 3);
        Assert.True(graph.EdgeCount == 3);
        Assert.True(graph.HasEdge(0, 1));
        Assert.True(graph.HasEdge(1, 2));
        Assert.True(graph.HasEdge(2, 0));
    }

    [Fact]
    public void ReadingStream_UndirectedGraph_ValidStructure()
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

        var graph = new UnweightedGraph(ms, RepresentationTypeEnum.LIST, false);

        Assert.True(graph.VertexCount == 3);
        Assert.True(graph.EdgeCount == 3);
        Assert.True(graph.HasEdge(0, 1));
        Assert.True(graph.HasEdge(1, 2));
        Assert.True(graph.HasEdge(2, 0));
        Assert.True(graph.HasEdge(1, 0));
        Assert.True(graph.HasEdge(2, 1));
        Assert.True(graph.HasEdge(0, 2));
    }
}