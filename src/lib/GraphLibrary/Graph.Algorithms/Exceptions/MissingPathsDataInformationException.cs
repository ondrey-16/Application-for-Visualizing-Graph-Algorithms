namespace AVGA.GraphLibrary;

public class MissingPathsDataInformationException : Exception
{
    public MissingPathsDataInformationException(int source)
        : base($"There is no path data information for {source} source vertex.")
    {}
}