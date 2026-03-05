namespace BackendTest.Application.Features.Purchases.AddPurchase;

public record AddPurchaseResponse(int Id, int CustomerId, IReadOnlyCollection<AddPurchaseResponse.PurchaseItem> Items)
{
    public record PurchaseItem(int ProductId, int Count);    
}
