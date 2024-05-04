namespace DeezNET.Exceptions;

public class InvalidTokenException : Exception
{
    public InvalidTokenException() { }
    public InvalidTokenException(string message) : base(message) { }
    public InvalidTokenException(string message, Exception inner) : base(message, inner) { }
}