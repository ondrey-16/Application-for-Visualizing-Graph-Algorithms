namespace AVGA.GraphLibrary;

public class Queue<T> : IOrderStructure<T>
{
    private System.Collections.Generic.Queue<T> _Q;

    public Queue()
    {
        _Q = new();
    }

    public void Push(T el) => _Q.Enqueue(el);
    public T Pop() => _Q.Dequeue();
    public T Peek() => _Q.Peek();
    public bool isEmpty() => _Q.Count == 0;
}