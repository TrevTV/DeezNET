namespace DeezNET.Exceptions;

public class InvalidURLException : Exception
{
    public InvalidURLException() { }
    public InvalidURLException(string message) : base(message) { }
    public InvalidURLException(string message, Exception inner) : base(message, inner) { }
}