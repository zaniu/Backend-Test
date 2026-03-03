namespace BackendTest.Application.Responses.Purchase;

public class GetAllPurchasesResponse : List<GetPurchaseByCustomerIdResponse>
{
    public GetAllPurchasesResponse()
    {
    }

    public GetAllPurchasesResponse(List<Model.Purchase> purchases)
    {
        AddRange(purchases.Select(purchase => new GetPurchaseByCustomerIdResponse(purchase)));
    }
}
