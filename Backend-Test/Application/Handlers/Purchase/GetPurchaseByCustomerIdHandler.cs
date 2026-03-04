using BackendTest.Application.Requests.Purchase;
using BackendTest.Application.Repositories;
using BackendTest.Application.Responses.Purchase;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Purchase;

public class GetPurchaseByCustomerIdHandler : IRequestHandler<GetPurchaseByCustomerIdRequest, GetPurchaseByCustomerIdResponse>
{
    private readonly IPurchaseRepository _purchaseRepository;

    public GetPurchaseByCustomerIdHandler(IPurchaseRepository purchaseRepository)
    {
        _purchaseRepository = purchaseRepository;
    }

    public Task<GetPurchaseByCustomerIdResponse> Handle(GetPurchaseByCustomerIdRequest request, CancellationToken cancellationToken)
    {
        var purchases = _purchaseRepository.GetByCustomerId(request.CustomerId, cancellationToken);

        if (purchases.Count == 0)
        {
            throw new NotFoundException();
        }

        return Task.FromResult(new GetPurchaseByCustomerIdResponse(purchases));
    }
}
