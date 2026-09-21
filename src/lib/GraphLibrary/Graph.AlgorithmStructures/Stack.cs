namespace AVGA.GraphLibrary;

/// <summary>
/// An order structure based on LIFO stack.
/// </summary>
/// <typeparam name="T">Type of stored elements.</typeparam>
public class Stack<T> : IOrderStructure<T>
{
    /// <summary>
    /// A LIFO stack.
    /// </summary>
    private System.Collections.Generic.Stack<T> _S;

    public Stack()
    {
        _S = new();
    }

    public void Push(T el) => _S.Push(el);
    public T Pop() => _S.Pop();
    public T Peek() => _S.Peek();
    public bool IsEmpty() => _S.Count == 0;
}