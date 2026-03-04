namespace BackendTest.Model;

public class PurchaseProductItem
{
    public int ProductId { get; init; }
    public int Count { get; init; }

    public PurchaseProductItem(int productId, int count)
    {
        ProductId = productId;
        Count = count;
    }
}
