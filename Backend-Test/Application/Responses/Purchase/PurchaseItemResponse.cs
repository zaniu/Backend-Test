namespace BackendTest.Application.Responses.Purchase;

public class PurchaseItemResponse
{
    public int Id { get; init; }
    public int CustomerId { get; init; }
    public List<int> ProductsIds { get; init; }

    public PurchaseItemResponse(Model.Purchase purchase)
    {
        Id = purchase.Id;
        CustomerId = purchase.CustomerId;
        ProductsIds = purchase.ProductsIds.ToList();
    }
}