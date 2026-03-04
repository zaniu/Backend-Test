namespace BackendTest.Application.Responses.Purchase;

public record GetAllPurchasesResponse(IReadOnlyCollection<GetPurchaseByCustomerIdResponse> Purchases)
{
    public GetAllPurchasesResponse(List<Model.Purchase> purchases)
        : this(purchases.Select(purchase => new GetPurchaseByCustomerIdResponse(purchase)).ToList())
    {
    }
}
