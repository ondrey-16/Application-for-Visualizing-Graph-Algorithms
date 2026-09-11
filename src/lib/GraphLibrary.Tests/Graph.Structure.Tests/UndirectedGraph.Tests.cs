namespace AVGA.GraphLibrary.Tests;

public class UndirectedGraphTests
{
    [Fact]
    public void ReadingStream_ForUndirectedWeightedGraph_ValidStructure()
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

        var graph = new UndirectedGraph<int>(ms, RepresentationTypeEnum.LIST);

        Assert.True(graph.VertexCount == 3);
        Assert.True(graph.EdgeCount == 3);
        Assert.True(graph.GetEdgeWeight(0, 1) == 3);
        Assert.True(graph.GetEdgeWeight(1, 0) == 3);
        Assert.True(graph.GetEdgeWeight(1, 2) == 2);
        Assert.True(graph.GetEdgeWeight(2, 1) == 2);
        Assert.True(graph.GetEdgeWeight(2, 0) == 1);
        Assert.True(graph.GetEdgeWeight(0, 2) == 1);
    }
}