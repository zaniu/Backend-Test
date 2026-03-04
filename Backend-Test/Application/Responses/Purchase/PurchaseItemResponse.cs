namespace BackendTest.Application.Responses.Purchase;

public record PurchaseItemResponse(int Id, int CustomerId, List<PurchaseProductItemResponse> Items)
{
    public PurchaseItemResponse(Model.Purchase purchase)
        : this(
            purchase.Id,
            purchase.CustomerId,
            purchase.Items
            .Select(item => new PurchaseProductItemResponse(item))
            .ToList())
    {
    }
}