namespace AVGA.GraphLibrary.Tests;

public class FlowAlgorithmsTests
{
    [Fact]
    public void FordFulkersonAlgorithm_ReturnsCorrectMaxFlow()
    {
        string s = """
        1 2 3
        1 3 2
        2 3 2
        2 4 1
        3 4 2
        """;

        MemoryStream ms = new();
        StreamWriter sw = new(ms);
        sw.Write(s);
        sw.Flush();
        ms.Position = 0;

        var network = new Network<int>(ms);

        int maxFlow = network.FordFulkerson(1, 4);

        Assert.True(maxFlow == 3);
        Assert.True(network.Flow.GetEdgeWeight(1, 2) == 3);
        Assert.True(network.Flow.GetEdgeWeight(1, 3) == 0);
        Assert.True(network.Flow.GetEdgeWeight(2, 3) == 2);
        Assert.True(network.Flow.GetEdgeWeight(2, 4) == 1);
        Assert.True(network.Flow.GetEdgeWeight(3, 4) == 2);
    }

    [Fact]
    public void MinCostMaxFlowAlgorithm_ReturnsCorrectMaxFlow()
    {
        string s = """
        1 2 3 0
        1 3 3 0
        2 4 3 1
        3 4 3 3
        4 5 4 0
        """;

        MemoryStream ms = new();
        StreamWriter sw = new(ms);
        sw.Write(s);
        sw.Flush();
        ms.Position = 0;

        var network = new CostNetwork<int, int>(ms);

        (int maxFlow, int minCost) = network.MinCostMaxFlow(1, 5);

        Assert.True(maxFlow == 4);
        Assert.True(minCost == 6);
        Assert.True(network.Flow.GetEdgeWeight(1, 2) == 3);
        Assert.True(network.Flow.GetEdgeWeight(1, 3) == 1);
        Assert.True(network.Flow.GetEdgeWeight(2, 4) == 3);
        Assert.True(network.Flow.GetEdgeWeight(3, 4) == 1);
        Assert.True(network.Flow.GetEdgeWeight(4, 5) == 4);
    }
}