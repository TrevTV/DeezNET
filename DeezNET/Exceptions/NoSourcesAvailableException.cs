namespace DeezNET.Exceptions;

public class NoSourcesAvailableException : Exception
{
    public NoSourcesAvailableException() { }
    public NoSourcesAvailableException(string message) : base(message) { }
    public NoSourcesAvailableException(string message, Exception inner) : base(message, inner) { }
}