namespace DeezNET.Exceptions;

public class InvalidIDException : Exception
{
    public InvalidIDException() { }
    public InvalidIDException(string message) : base(message) { }
    public InvalidIDException(string message, Exception inner) : base(message, inner) { }
}