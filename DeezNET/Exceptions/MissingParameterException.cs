namespace DeezNET.Exceptions;

public class MissingParameterException : Exception
{
    public MissingParameterException() { }
    public MissingParameterException(string message) : base(message) { }
    public MissingParameterException(string message, Exception inner) : base(message, inner) { }
}