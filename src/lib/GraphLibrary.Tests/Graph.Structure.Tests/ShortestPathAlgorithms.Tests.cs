namespace AVGA.GraphLibrary.Tests;

public class ShortestPathAlgorithmsTests
{
    [Fact]
    public void BellmanFord_ReturnsCorrectPath()
    {
        string s = """
        0 1 1
        0 2 4
        1 2 2
        """;

        MemoryStream ms = new();
        StreamWriter sw = new(ms);
        sw.Write(s);
        sw.Flush();
        ms.Position = 0;

        var graph = new DirectedGraph<int>(ms);

        var paths = graph.BellmanFord(0);
        var path = paths.GetPath(0, 2);

        Assert.True(path.Count == 3);
        Assert.True(path[0] == 0);
        Assert.True(path[1] == 1);
        Assert.True(path[2] == 2);
        Assert.True(paths.GetDistance(0, 2) == 3);
    }

    [Fact]
    public void Dijkstra_ReturnsCorrectPath()
    {
        string s = """
        0 1 1
        0 2 4
        1 2 2
        """;

        MemoryStream ms = new();
        StreamWriter sw = new(ms);
        sw.Write(s);
        sw.Flush();
        ms.Position = 0;

        var graph = new DirectedGraph<int>(ms);

        var paths = graph.Dijkstra(0);
        var path = paths.GetPath(0, 2);

        Assert.True(path.Count == 3);
        Assert.True(path[0] == 0);
        Assert.True(path[1] == 1);
        Assert.True(path[2] == 2);
        Assert.True(paths.GetDistance(0, 2) == 3);
    }

    [Fact]
    public void FloydWarshall_ReturnsCorrectPaths()
    {
        string s = """
        0 1 1
        0 2 4
        1 2 2
        2 0 5
        2 3 2
        3 0 2
        """;

        MemoryStream ms = new();
        StreamWriter sw = new(ms);
        sw.Write(s);
        sw.Flush();
        ms.Position = 0;

        var graph = new DirectedGraph<int>(ms);

        var paths = graph.FloydWarshall();
        var path02 = paths.GetPath(0, 2);

        Assert.True(path02.Count == 3);
        Assert.True(path02[0] == 0);
        Assert.True(path02[1] == 1);
        Assert.True(path02[2] == 2);
        Assert.True(paths.GetDistance(0, 2) == 3);

        var path20 = paths.GetPath(2, 0);

        Assert.True(path20.Count == 3);
        Assert.True(path20[0] == 2);
        Assert.True(path20[1] == 3);
        Assert.True(path20[2] == 0);
        Assert.True(paths.GetDistance(2, 0) == 4);
    }

    [Fact]
    public void Johnson_ReturnsCorrectPaths()
    {
        string s = """
        0 1 1
        0 2 4
        1 2 2
        2 0 5
        2 3 2
        3 0 2
        """;

        MemoryStream ms = new();
        StreamWriter sw = new(ms);
        sw.Write(s);
        sw.Flush();
        ms.Position = 0;

        var graph = new DirectedGraph<int>(ms);

        var paths = graph.Johnson();
        var path02 = paths.GetPath(0, 2);

        Assert.True(path02.Count == 3);
        Assert.True(path02[0] == 0);
        Assert.True(path02[1] == 1);
        Assert.True(path02[2] == 2);
        Assert.True(paths.GetDistance(0, 2) == 3);

        var path20 = paths.GetPath(2, 0);

        Assert.True(path20.Count == 3);
        Assert.True(path20[0] == 2);
        Assert.True(path20[1] == 3);
        Assert.True(path20[2] == 0);
        Assert.True(paths.GetDistance(2, 0) == 4);
    }
}