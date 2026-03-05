namespace BackendTest.Exceptions;

public class DuplicateException : Exception
{
    public DuplicateException()
        : base("Item already exists")
    {
    }
}
