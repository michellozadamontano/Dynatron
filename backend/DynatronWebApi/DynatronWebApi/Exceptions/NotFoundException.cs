namespace DynatronWebApi.Exceptions;

/// <summary>
///     Exception that is thrown when an entity is not found
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    ///     Default constructor
    /// </summary>
    /// <param name="name"></param>
    /// <param name="key"></param>
    public NotFoundException(string name, object key) : base($"{name} ({key}) was not found")
    {
    }

    /// <summary>
    ///     Default constructor
    /// </summary>
    /// <param name="name"></param>
    public NotFoundException(string name) : base($"{name} was not found")
    {
    }
}