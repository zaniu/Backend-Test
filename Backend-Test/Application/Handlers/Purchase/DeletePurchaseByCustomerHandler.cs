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

    public Task<Unit> Handle(DeletePurchaseByCustomerRequest request, CancellationToken cancellationToken)
    {
        var purchase = _purchaseRepository.GetByCustomerId(request.CustomerId, cancellationToken);

        if (purchase == null)
        {
            throw new NotFoundException();
        }

        _purchaseRepository.DeleteByCustomerId(request.CustomerId, cancellationToken);
        return Task.FromResult(Unit.Value);
    }
}
