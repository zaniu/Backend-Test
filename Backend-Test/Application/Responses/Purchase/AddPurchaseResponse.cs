namespace BackendTest.Application.Responses.Purchase;

public record AddPurchaseResponse(int Id, int CustomerId, List<PurchaseProductItemResponse> Items)
{
    public AddPurchaseResponse(Model.Purchase purchase)
        : this(
            purchase.Id,
            purchase.CustomerId,
            purchase.Items
            .Select(item => new PurchaseProductItemResponse(item))
            .ToList())
    {
    }
}
