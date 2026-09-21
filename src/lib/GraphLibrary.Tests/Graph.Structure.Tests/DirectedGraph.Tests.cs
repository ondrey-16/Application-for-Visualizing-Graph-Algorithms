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

    [Fact]
    public void BFS_ForDirectedGraph_ReturnsCorrectEdges_SearchFromAndAll()
    {
        string s = """
        0 1
        0 3
        1 2
        4 5
        """;

        MemoryStream ms = new();
        StreamWriter sw = new(ms);
        sw.Write(s);
        sw.Flush();
        ms.Position = 0;

        var graph = new DirectedGraph(ms);

        var edgesFrom0 = graph.BFS().SearchFrom(0).ToList();

        Assert.True(edgesFrom0.Count == 3);
        Assert.True(edgesFrom0[0].From == 0 && edgesFrom0[0].To == 1);
        Assert.True(edgesFrom0[1].From == 0 && edgesFrom0[1].To == 3);
        Assert.True(edgesFrom0[2].From == 1 && edgesFrom0[2].To == 2);

        var allEdges = graph.BFS().SearchAllEdges().ToList();

        Assert.True(allEdges.Count == 4);
        Assert.Contains(allEdges, e => e.From == 4 && e.To == 5);
    }

    [Fact]
    public void DFS_ForDirectedGraph_ReturnsCorrectEdges_SearchFromAndAll()
    {
        string s = """
        0 1
        0 3
        1 2
        4 5
        """;

        MemoryStream ms = new();
        StreamWriter sw = new(ms);
        sw.Write(s);
        sw.Flush();
        ms.Position = 0;

        var graph = new DirectedGraph(ms);

        var edgesFrom0 = graph.DFS().SearchFrom(0).ToList();

        Assert.True(edgesFrom0.Count == 3);
        Assert.True(edgesFrom0[0].From == 0 && edgesFrom0[0].To == 1);
        Assert.True(edgesFrom0[1].From == 1 && edgesFrom0[1].To == 2);
        Assert.True(edgesFrom0[2].From == 0 && edgesFrom0[2].To == 3);

        var allEdges = graph.DFS().SearchAllEdges().ToList();

        Assert.True(allEdges.Count == 4);
        Assert.Contains(allEdges, e => e.From == 4 && e.To == 5);
    }

    [Fact]
    public void BFS_ForWeightedDirectedGraph_ReturnsCorrectEdges_SearchFromAndAll()
    {
        string s = """
        0 1 1
        0 3 2
        1 2 3
        4 5 4
        """;

        MemoryStream ms = new();
        StreamWriter sw = new(ms);
        sw.Write(s);
        sw.Flush();
        ms.Position = 0;

        var graph = new DirectedGraph<int>(ms);

        var edgesFrom0 = graph.BFS().SearchFrom(0).ToList();

        Assert.True(edgesFrom0.Count == 3);
        Assert.True(edgesFrom0[0].From == 0 && edgesFrom0[0].To == 1 && edgesFrom0[0].Weight == 1);
        Assert.True(edgesFrom0[1].From == 0 && edgesFrom0[1].To == 3 && edgesFrom0[1].Weight == 2);
        Assert.True(edgesFrom0[2].From == 1 && edgesFrom0[2].To == 2 && edgesFrom0[2].Weight == 3);

        var allEdges = graph.BFS().SearchAllEdges().ToList();

        Assert.True(allEdges.Count == 4);
        Assert.Contains(allEdges, e => e.From == 4 && e.To == 5 && e.Weight == 4);
    }

    [Fact]
    public void DFS_ForWeightedDirectedGraph_ReturnsCorrectEdges_SearchFromAndAll()
    {
        string s = """
        0 1 1
        0 3 2
        1 2 3
        4 5 4
        """;

        MemoryStream ms = new();
        StreamWriter sw = new(ms);
        sw.Write(s);
        sw.Flush();
        ms.Position = 0;

        var graph = new DirectedGraph<int>(ms);

        var edgesFrom0 = graph.DFS().SearchFrom(0).ToList();

        Assert.True(edgesFrom0.Count == 3);
        Assert.True(edgesFrom0[0].From == 0 && edgesFrom0[0].To == 1 && edgesFrom0[0].Weight == 1);
        Assert.True(edgesFrom0[1].From == 1 && edgesFrom0[1].To == 2 && edgesFrom0[1].Weight == 3);
        Assert.True(edgesFrom0[2].From == 0 && edgesFrom0[2].To == 3 && edgesFrom0[2].Weight == 2);

        var allEdges = graph.DFS().SearchAllEdges().ToList();

        Assert.True(allEdges.Count == 4);
        Assert.Contains(allEdges, e => e.From == 4 && e.To == 5 && e.Weight == 4);
    }
}