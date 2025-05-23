namespace NotifyHub.Domain.Exceptions;

public class NoActiveDeviceException : ApplicationException
{
    public NoActiveDeviceException(string message) : base(message) { }
}
