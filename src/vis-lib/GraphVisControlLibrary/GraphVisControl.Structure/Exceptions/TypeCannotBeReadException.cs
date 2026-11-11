namespace AVGA.GraphVisControlLibrary;

public class TypeCannotBeReadException<T> : Exception
{
    public TypeCannotBeReadException() :
        base($"A variable of type: {typeof(T)}") {}
}