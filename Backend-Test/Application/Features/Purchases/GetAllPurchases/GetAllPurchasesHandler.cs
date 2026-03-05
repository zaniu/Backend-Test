using MediatR;

namespace BackendTest.Application.Features.Purchases.GetAllPurchases;

public class GetAllPurchasesHandler : IRequestHandler<GetAllPurchasesRequest, GetAllPurchasesResponse>
{
    private readonly IPurchaseRepository _purchaseRepository;

    public GetAllPurchasesHandler(IPurchaseRepository purchaseRepository)
    {
        _purchaseRepository = purchaseRepository;
    }

    public async Task<GetAllPurchasesResponse> Handle(GetAllPurchasesRequest request, CancellationToken cancellationToken)
    {
        var purchases = await _purchaseRepository.GetAll(cancellationToken);
        return new GetAllPurchasesResponse(
            purchases.Select(p => new GetAllPurchasesResponse.Purchase(
                p.Id,
                p.CustomerId,
                p.Items.Select(i => new GetAllPurchasesResponse.Purchase.PurchaseItem(i.ProductId, i.Count)).ToList()))
            .ToList());
    }
}
