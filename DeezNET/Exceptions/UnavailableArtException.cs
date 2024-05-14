namespace DeezNET.Exceptions;

public class UnavailableArtException : Exception
{
    public UnavailableArtException() { }
    public UnavailableArtException(string message) : base(message) { }
    public UnavailableArtException(string message, Exception inner) : base(message, inner) { }
}