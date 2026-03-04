using BackendTest.Application.Requests.Purchase;
using BackendTest.Application.Repositories;
using BackendTest.Application.Responses.Purchase;
using MediatR;

namespace BackendTest.Application.Handlers.Purchase;

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
        return new GetAllPurchasesResponse(purchases);
    }
}
