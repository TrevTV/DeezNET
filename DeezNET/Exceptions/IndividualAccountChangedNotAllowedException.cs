namespace DeezNET.Exceptions;

public class IndividualAccountChangedNotAllowedException : Exception
{
    public IndividualAccountChangedNotAllowedException() { }
    public IndividualAccountChangedNotAllowedException(string message) : base(message) { }
    public IndividualAccountChangedNotAllowedException(string message, Exception inner) : base(message, inner) { }
}