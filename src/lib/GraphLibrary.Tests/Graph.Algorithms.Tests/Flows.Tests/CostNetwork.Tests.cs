using AVGA.GraphLibrary;

public class CostNetworkTests
{
    [Fact]
    public void ReadingFromStream_CostNetwork_ValidStream()
    {
        string s = """
        0 1 1 1
        0 3 2 2
        1 2 3 3
        """;

        MemoryStream ms = new();
        StreamWriter sw = new(ms);
        sw.Write(s);
        sw.Flush();
        ms.Position = 0;

        var network = new CostNetwork<int, int>(ms);
        
        Assert.True(network.VertexCount == 4);
        Assert.True(network.Flow.VertexCount == 4);
        Assert.True(network.EdgeCount == 3);
        Assert.True(network.Flow.EdgeCount == 6);
        Assert.True(network.GetEdgeWeight(0, 1) == (1, 1));
        Assert.True(network.GetEdgeWeight(0, 3) == (2, 2));
        Assert.True(network.GetEdgeWeight(1, 2) == (3, 3));
        Assert.True(network.Flow.GetEdgeWeight(0, 1) == 0);
        Assert.True(network.Flow.GetEdgeWeight(0, 3) == 0);
        Assert.True(network.Flow.GetEdgeWeight(1, 2) == 0);
    }

    [Fact]
    public void CostNetwork_AddEdge_RemoveEdge_AddsRemovesEdgeProperly()
    {
        string s = """
        0 1 1 1
        0 3 2 2
        1 2 3 3
        """;

        MemoryStream ms = new();
        StreamWriter sw = new(ms);
        sw.Write(s);
        sw.Flush();
        ms.Position = 0;

        var network = new CostNetwork<int, int>(ms);

        network.AddEdge(0, 2, (1, 2));

        Assert.True(network.GetEdgeWeight(0, 2) == (1, 2));
        Assert.True(network.Flow.GetEdgeWeight(0, 2) == 0);

        network.RemoveEdge(0, 2);

        Assert.True(!network.HasEdge(0, 2));
        Assert.True(!network.Flow.HasEdge(0, 2));
    }
}