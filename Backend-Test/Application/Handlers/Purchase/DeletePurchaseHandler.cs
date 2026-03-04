using BackendTest.Application.Requests.Purchase;
using BackendTest.Application.Repositories;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Purchase;

public class DeletePurchaseHandler : IRequestHandler<DeletePurchaseRequest, Unit>
{
    private readonly IPurchaseRepository _purchaseRepository;

    public DeletePurchaseHandler(IPurchaseRepository purchaseRepository)
    {
        _purchaseRepository = purchaseRepository;
    }

    public Task<Unit> Handle(DeletePurchaseRequest request, CancellationToken cancellationToken)
    {
        if (!_purchaseRepository.Exists(request.Id, cancellationToken))
        {
            throw new NotFoundException();
        }

        _purchaseRepository.DeleteById(request.Id, cancellationToken);
        return Task.FromResult(Unit.Value);
    }
}
