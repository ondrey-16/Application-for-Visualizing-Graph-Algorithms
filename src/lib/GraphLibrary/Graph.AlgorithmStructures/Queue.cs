namespace AVGA.GraphLibrary;

/// <summary>
/// An order structure based on FIFO queue.
/// </summary>
/// <typeparam name="T">Type of stored elements.</typeparam>
public class Queue<T> : IOrderStructure<T>
{
    /// <summary>
    /// A FIFO queue.
    /// </summary>
    private System.Collections.Generic.Queue<T> _Q;

    public Queue()
    {
        _Q = new();
    }

    public void Push(T el) => _Q.Enqueue(el);
    public T Pop() => _Q.Dequeue();
    public T Peek() => _Q.Peek();
    public bool IsEmpty() => _Q.Count == 0;
}