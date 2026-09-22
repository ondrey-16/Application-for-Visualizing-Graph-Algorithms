namespace AVGA.GraphLibrary.Tests;

public class GraphTests
{
    [Fact]
    public void ReadingStream_ForUnweightedGraph_ValidStructure_AndChangingRepresentation()
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

        var graph = new Graph(ms);

        Assert.True(graph.VertexCount == 3);
        Assert.True(graph.EdgeCount == 3);

        var cloned = (Graph) graph.Clone();

        Assert.True(cloned.HasEdge(0, 1));
        Assert.True(cloned.HasEdge(1, 0));
        Assert.True(cloned.HasEdge(1, 2));
        Assert.True(cloned.HasEdge(2, 1));
        Assert.True(cloned.HasEdge(2, 0));
        Assert.True(cloned.HasEdge(0, 2));
    }

    [Fact]
    public void ReadingStream_ForWeightedGraph_ValidStructure_AndChangingRepresentation()
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

        var graph = new Graph<int>(ms);

        Assert.True(graph.VertexCount == 3);
        Assert.True(graph.EdgeCount == 3);
        Assert.True(graph.GetEdgeWeight(0, 1) == 3);
        Assert.True(graph.GetEdgeWeight(1, 0) == 3);
        Assert.True(graph.GetEdgeWeight(1, 2) == 2);
        Assert.True(graph.GetEdgeWeight(2, 1) == 2);
        Assert.True(graph.GetEdgeWeight(2, 0) == 1);
        Assert.True(graph.GetEdgeWeight(0, 2) == 1);

        var cloned = (Graph<int>) graph.Clone();

        Assert.True(cloned.GetEdgeWeight(0, 1) == 3);
        Assert.True(cloned.GetEdgeWeight(1, 2) == 2);
        Assert.True(cloned.GetEdgeWeight(2, 0) == 1);
    }

    [Fact]
    public void BFS_ForGraph_ReturnsCorrectEdges_SearchFromAndAll()
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

        var graph = new Graph(ms);

        var edgesFrom0 = graph.BFS().SearchFrom(0).ToList();
        int idx12 = edgesFrom0.IndexOf(new Edge(1, 2));
        int idx03 = edgesFrom0.IndexOf(new Edge(0, 3));

        Assert.True(edgesFrom0.Count == 6);
        Assert.True(edgesFrom0[0].From == 0 && edgesFrom0[0].To == 1);
        Assert.True(idx12 > idx03);

        var allEdges = graph.BFS().SearchAllEdges().ToList();

        Assert.True(allEdges.Count == 8);
        Assert.Contains(allEdges, e => e.From == 4 && e.To == 5);
    }

    [Fact]
    public void DFS_ForGraph_ReturnsCorrectEdges_SearchFromAndAll()
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

        var graph = new Graph(ms);

        var edgesFrom0 = graph.DFS().SearchFrom(0).ToList();
        int idx12 = edgesFrom0.IndexOf(new Edge(1, 2));
        int idx03 = edgesFrom0.IndexOf(new Edge(0, 3));

        Assert.True(edgesFrom0.Count == 6);
        Assert.True(edgesFrom0[0].From == 0 && edgesFrom0[0].To == 1);
        Assert.True(idx12 < idx03);

        var allEdges = graph.DFS().SearchAllEdges().ToList();

        Assert.True(allEdges.Count == 8);
        Assert.Contains(allEdges, e => e.From == 4 && e.To == 5);
    }

    [Fact]
    public void BFS_ForWeightedGraph_ReturnsCorrectEdges_SearchFromAndAll()
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

        var graph = new Graph<int>(ms);

        var edgesFrom0 = graph.BFS().SearchFrom(0).ToList();
        int idx12 = edgesFrom0.IndexOf(new Edge<int>(1, 2, 3));
        int idx03 = edgesFrom0.IndexOf(new Edge<int>(0, 3, 2));

        Assert.True(edgesFrom0.Count == 6);
        Assert.True(edgesFrom0[0].From == 0 && edgesFrom0[0].To == 1 && edgesFrom0[0].Weight == 1);
        Assert.True(idx12 > idx03);

        var allEdges = graph.BFS().SearchAllEdges().ToList();

        Assert.True(allEdges.Count == 8);
        Assert.Contains(allEdges, e => e.From == 4 && e.To == 5 && e.Weight == 4);
    }

    [Fact]
    public void DFS_ForWeightedGraph_ReturnsCorrectEdges_SearchFromAndAll()
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

        var graph = new Graph<int>(ms);

        var edgesFrom0 = graph.DFS().SearchFrom(0).ToList();
        int idx12 = edgesFrom0.IndexOf(new Edge<int>(1, 2, 3));
        int idx03 = edgesFrom0.IndexOf(new Edge<int>(0, 3, 2));

        Assert.True(edgesFrom0.Count == 6);
        Assert.True(edgesFrom0[0].From == 0 && edgesFrom0[0].To == 1 && edgesFrom0[0].Weight == 1);
        Assert.True(idx12 < idx03);

        var allEdges = graph.DFS().SearchAllEdges().ToList();

        Assert.True(allEdges.Count == 8);
        Assert.Contains(allEdges, e => e.From == 4 && e.To == 5 && e.Weight == 4);
    }
}