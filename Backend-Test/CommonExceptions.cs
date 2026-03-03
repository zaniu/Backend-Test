namespace BackendTest;

public class CommonExceptions
{
    /// <summary>
    /// If the item doesn't match what we want to update
    /// </summary>
    public void IdDoesNotMatch()
    {
        throw new Exception("Id doesn't match item");
    }

    /// <summary>
    /// If an item doesn't exist
    /// </summary>
    public void ItemNotExists()
    {
        throw new Exception("Item does not exist");
    }

    /// <summary>
    /// If an item already exists
    /// </summary>
    public void ItemAlreadyExists()
    {
        throw new Exception("Item already exists");
    }
}
