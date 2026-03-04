namespace BackendTest.Application.Responses.Purchase;

public record GetPurchaseByCustomerIdResponse(IReadOnlyCollection<PurchaseItemResponse> Purchases)
{
    public GetPurchaseByCustomerIdResponse(List<Model.Purchase> purchases)
        : this(purchases.Select(purchase => new PurchaseItemResponse(purchase)).ToList())
    {
    }
}
