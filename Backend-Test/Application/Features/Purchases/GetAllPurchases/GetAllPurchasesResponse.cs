namespace BackendTest.Application.Features.Purchases.GetAllPurchases;

public record GetAllPurchasesResponse(IReadOnlyCollection<GetAllPurchasesResponse.Purchase> Purchases)
{
    public record Purchase(int Id, int CustomerId, IReadOnlyCollection<Purchase.PurchaseItem> Items)
    {
        public record PurchaseItem(int ProductId, int Count);
    }
}
