namespace BackendTest.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException()
        : base("Item does not exist")
    {
    }
}