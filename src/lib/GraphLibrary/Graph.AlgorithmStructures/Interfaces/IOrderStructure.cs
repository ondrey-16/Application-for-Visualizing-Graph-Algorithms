namespace AVGA.GraphLibrary;

public interface IOrderStructure<T>
{
    public void Push(T el);
    public T Pop();
    public T Peek();
    public bool isEmpty();
}