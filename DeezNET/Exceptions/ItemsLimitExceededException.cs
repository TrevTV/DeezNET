namespace DeezNET.Exceptions;

public class ItemsLimitExceededException : Exception
{
    public ItemsLimitExceededException() { }
    public ItemsLimitExceededException(string message) : base(message) { }
    public ItemsLimitExceededException(string message, Exception inner) : base(message, inner) { }
}