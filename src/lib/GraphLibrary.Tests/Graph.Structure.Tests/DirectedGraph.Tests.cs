namespace AVGA.GraphLibrary.Tests;

public class DirectedGraphTests
{
    [Fact]
    public void ReadingStream_ValidStructure_WithCorrectCloneProcedure_AndChangingRepresentation()
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

        var graph = new DirectedGraph<int>(ms, RepresentationTypeEnum.LIST);

        Assert.True(graph.VertexCount == 3);
        Assert.True(graph.EdgeCount == 3);
        Assert.True(graph.GetEdgeWeight(0, 1) == 3);
        Assert.True(graph.GetEdgeWeight(1, 2) == 2);
        Assert.True(graph.GetEdgeWeight(2, 0) == 1);

        var cloned = (DirectedGraph<int>) graph.Clone();

        Assert.True(cloned.GetEdgeWeight(0, 1) == 3);
        Assert.True(cloned.GetEdgeWeight(1, 2) == 2);
        Assert.True(cloned.GetEdgeWeight(2, 0) == 1);

        cloned.ChangeToMatrixRepresentation();

        Assert.True(cloned.GetEdgeWeight(0, 1) == 3);
        Assert.True(cloned.GetEdgeWeight(1, 2) == 2);
        Assert.True(cloned.GetEdgeWeight(2, 0) == 1);

        cloned.ChangeToListRepresentation();

        Assert.True(cloned.GetEdgeWeight(0, 1) == 3);
        Assert.True(cloned.GetEdgeWeight(1, 2) == 2);
        Assert.True(cloned.GetEdgeWeight(2, 0) == 1);
    }
}