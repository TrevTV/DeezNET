namespace DeezNET.Exceptions;

public class DataException : Exception
{
    public DataException() { }
    public DataException(string message) : base(message) { }
    public DataException(string message, Exception inner) : base(message, inner) { }
}