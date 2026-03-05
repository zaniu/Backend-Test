using MediatR;

namespace BackendTest.Application.Features.Purchases.GetPurchaseByCustomerId;

public class GetPurchaseByCustomerIdHandler : IRequestHandler<GetPurchaseByCustomerIdRequest, GetPurchaseByCustomerIdResponse>
{
    private readonly IPurchaseRepository _purchaseRepository;

    public GetPurchaseByCustomerIdHandler(IPurchaseRepository purchaseRepository)
    {
        _purchaseRepository = purchaseRepository;
    }

    public async Task<GetPurchaseByCustomerIdResponse> Handle(GetPurchaseByCustomerIdRequest request, CancellationToken cancellationToken)
    {
        var purchases = await _purchaseRepository.GetByCustomerId(request.CustomerId, cancellationToken);

        return new GetPurchaseByCustomerIdResponse(
            purchases.Select(p => new GetPurchaseByCustomerIdResponse.Purchase(
                p.Id,
                p.CustomerId,
                p.Items.Select(i => new GetPurchaseByCustomerIdResponse.Purchase.PurchaseItem(
                    i.ProductId,
                    i.Count))
                .ToList()))
            .ToList());
    }
}
