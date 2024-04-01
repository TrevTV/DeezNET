namespace DeezNET.Exceptions;

public class InvalidARLException : Exception
{
    public InvalidARLException() { }
    public InvalidARLException(string message) : base(message) { }
    public InvalidARLException(string message, Exception inner) : base(message, inner) { }
}