using BackendTest.Application.Requests.Purchase;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Purchase;

public class DeletePurchaseByCustomerHandler : IRequestHandler<DeletePurchaseByCustomerRequest, Unit>
{
    private readonly Data _data;

    public DeletePurchaseByCustomerHandler(Data data)
    {
        _data = data;
    }

    public Task<Unit> Handle(DeletePurchaseByCustomerRequest request, CancellationToken cancellationToken)
    {
        var purchase = _data.purchases.FirstOrDefault(s => s.CustomerId == request.CustomerId);

        if (purchase == null)
        {
            throw new NotFoundException();
        }

        _data.purchases.Remove(purchase!);
        return Task.FromResult(Unit.Value);
    }
}
