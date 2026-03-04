using BackendTest.Application.Requests.Purchase;
using BackendTest.Application.Repositories;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Purchase;

public class DeletePurchaseByCustomerHandler : IRequestHandler<DeletePurchaseByCustomerRequest, Unit>
{
    private readonly IPurchaseRepository _purchaseRepository;

    public DeletePurchaseByCustomerHandler(IPurchaseRepository purchaseRepository)
    {
        _purchaseRepository = purchaseRepository;
    }

    public async Task<Unit> Handle(DeletePurchaseByCustomerRequest request, CancellationToken cancellationToken)
    {
        var wasDeleted = await _purchaseRepository.TryDeleteByCustomerId(request.CustomerId, cancellationToken);

        if (!wasDeleted)
        {
            throw new NotFoundException();
        }

        return Unit.Value;
    }
}
