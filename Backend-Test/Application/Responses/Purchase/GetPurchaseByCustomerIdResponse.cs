namespace BackendTest.Application.Responses.Purchase;

public class GetPurchaseByCustomerIdResponse
{
    public IReadOnlyCollection<PurchaseItemResponse> Purchases { get; init; }

    public GetPurchaseByCustomerIdResponse(List<Model.Purchase> purchases)
    {
        Purchases = purchases.Select(purchase => new PurchaseItemResponse(purchase)).ToList();
    }
}
