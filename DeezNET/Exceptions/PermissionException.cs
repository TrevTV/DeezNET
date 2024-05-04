namespace DeezNET.Exceptions;

public class PermissionException : Exception
{
    public PermissionException() { }
    public PermissionException(string message) : base(message) { }
    public PermissionException(string message, Exception inner) : base(message, inner) { }
}