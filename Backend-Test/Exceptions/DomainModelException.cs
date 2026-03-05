namespace BackendTest.Exceptions;

public class DomainModelException : Exception
{
    public DomainModelException(string message)
        : base(message)
    {
    }
}
