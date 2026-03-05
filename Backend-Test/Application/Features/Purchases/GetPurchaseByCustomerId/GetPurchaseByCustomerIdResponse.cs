namespace BackendTest.Application.Features.Purchases.GetPurchaseByCustomerId;

public record GetPurchaseByCustomerIdResponse(IReadOnlyCollection<GetPurchaseByCustomerIdResponse.Purchase> Purchases)
{
    public record Purchase(int Id, int CustomerId, IReadOnlyCollection<Purchase.PurchaseItem> Items)
    {
        public record PurchaseItem(int ProductId, int Count);
    }
}
