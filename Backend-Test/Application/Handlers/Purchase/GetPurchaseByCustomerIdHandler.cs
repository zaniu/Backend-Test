using BackendTest.Application.Requests.Purchase;
using BackendTest.Application.Repositories;
using BackendTest.Application.Responses.Purchase;
using MediatR;

namespace BackendTest.Application.Handlers.Purchase;

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

        return new GetPurchaseByCustomerIdResponse(purchases);
    }
}
