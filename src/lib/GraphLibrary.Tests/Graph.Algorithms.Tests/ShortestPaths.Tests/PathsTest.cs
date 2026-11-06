namespace AVGA.GraphLibrary.Tests;

public class PathsTests
{
    [Fact]
    public void GetPath_ValidEdgesAndOrder()
    {
        Paths<int> paths = new(4);
        paths.InitSourceRow(0);
        paths.AddPathNode(0, 0, 0, 0);
        paths.AddPathNode(0, 1, 0, 1);
        paths.AddPathNode(0, 2, 1, 2);
        paths.AddPathNode(0, 3, 2, 3);

        var path0 = paths.GetPath(0, 0);
        Assert.True(path0.Count == 0);

        var path1 = paths.GetPath(0, 2);
        Assert.True(path1.Count == 3);
        Assert.True(path1[0] == 0);
        Assert.True(path1[1] == 1);
        Assert.True(path1[2] == 2);

        var path2 = paths.GetPath(0, 3);
        Assert.True(path2.Count == 4);
        Assert.True(path2[0] == 0);
        Assert.True(path2[1] == 1);
        Assert.True(path2[2] == 2);
        Assert.True(path2[3] == 3);
    }

    [Fact]
    public void GetPath_ThrowsException_When_PathDoesNotExist()
    {
        Paths<int> paths = new(4);
        paths.InitSourceRow(0);

        Assert.Throws<NonExistingPathException>(() => paths.GetPath(0, 1));
    }

    [Fact]
    public void GetPath_ThrowsException_When_InfiniteCycle()
    {
        Paths<int> paths = new(4);
        paths.InitSourceRow(0);
        paths.AddPathNode(0, 1, 2, 1);
        paths.AddPathNode(0, 2, 3, 1);
        paths.AddPathNode(0, 3, 1, 1);

        Assert.Throws<InfiniteCycleException>(() => paths.GetPath(0, 1));
    }

    [Fact]
    public void GetPreviousDistance_ThrowsException_When_NotRowInitted()
    {
        Paths<int> paths = new(4);
        Assert.Throws<MissingPathsDataInformationException>(() => paths.GetPrevious(0, 1));
        Assert.Throws<MissingPathsDataInformationException>(() => paths.GetDistance(0, 1));
    }
}