using MediatR;

namespace BackendTest.Application.Features.Purchases.AddPurchase;

public record AddPurchaseRequest(int Id, int CustomerId, IReadOnlyCollection<AddPurchaseRequest.PurchaseItem> Items) : IRequest<AddPurchaseResponse>
{
    public record PurchaseItem(int ProductId, int Count);
}
