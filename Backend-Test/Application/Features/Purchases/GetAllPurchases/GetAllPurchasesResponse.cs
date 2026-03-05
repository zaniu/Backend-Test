using BackendTest.Contracts;

namespace BackendTest.Application.Features.Purchases.GetAllPurchases;

public record GetAllPurchasesResponse(IReadOnlyCollection<GetAllPurchasesResponse.Purchase> Value) : CollectionResponse<GetAllPurchasesResponse.Purchase>(Value)
{

    public record Purchase(int Id, int CustomerId, IReadOnlyCollection<Purchase.PurchaseItem> Items)
    {
        public record PurchaseItem(int ProductId, int Count);
    }
}
