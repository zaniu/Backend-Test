namespace BackendTest.Application.Responses.Purchase;

public record GetAllPurchasesResponse(IReadOnlyCollection<PurchaseItemResponse> Purchases)
{
    public GetAllPurchasesResponse(List<Model.Purchase> purchases)
        : this(purchases.Select(purchase => new PurchaseItemResponse(purchase)).ToList())
    {
    }
}
