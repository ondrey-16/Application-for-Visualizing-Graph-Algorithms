namespace AVGA.GraphLibrary;

public class Stack<T> : IOrderStructure<T>
{
    private System.Collections.Generic.Stack<T> _S;

    public Stack()
    {
        _S = new();
    }

    public void Push(T el) => _S.Push(el);
    public T Pop() => _S.Pop();
    public T Peek() => _S.Peek();
    public bool isEmpty() => _S.Count == 0;
}