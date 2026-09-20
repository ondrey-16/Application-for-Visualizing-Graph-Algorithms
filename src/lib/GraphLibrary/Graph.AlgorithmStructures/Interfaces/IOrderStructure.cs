namespace AVGA.GraphLibrary;

/// <summary>
/// An interface of structures storing and returning the elements in a special order.
/// </summary>
/// <typeparam name="T">Type of stored elements.</typeparam>
public interface IOrderStructure<T>
{
    /// <summary>
    /// Adds an element to the structure.
    /// </summary>
    /// <param name="el">An element to add.</param>
    public void Push(T el);

    /// <summary>
    /// Removes an element first in order.
    /// </summary>
    /// <returns>An element first in order.</returns>
    public T Pop();
    /// <summary>
    /// Returns an element first in order without removing. 
    /// </summary>
    public T Peek();

    /// <summary>
    /// Checks if the structure is empty.
    /// </summary>
    /// <returns>True if the structure is empty, otherwise returns false.</returns>
    public bool IsEmpty();
}