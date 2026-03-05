using BackendTest.Application.Features.Purchases.DeletePurchase;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Features.Purchases;

public class DeletePurchaseHandler : IRequestHandler<DeletePurchaseRequest, Unit>
{
    private readonly IPurchaseRepository _purchaseRepository;

    public DeletePurchaseHandler(IPurchaseRepository purchaseRepository)
    {
        _purchaseRepository = purchaseRepository;
    }

    public async Task<Unit> Handle(DeletePurchaseRequest request, CancellationToken cancellationToken)
    {
        var wasDeleted = await _purchaseRepository.TryDeleteById(request.Id, cancellationToken);

        if (!wasDeleted)
        {
            throw new NotFoundException();
        }

        return Unit.Value;
    }
}
