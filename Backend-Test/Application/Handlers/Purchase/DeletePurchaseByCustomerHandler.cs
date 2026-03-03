using BackendTest.Application.Requests.Purchase;
using MediatR;

namespace BackendTest.Application.Handlers.Purchase;

public class DeletePurchaseByCustomerHandler : IRequestHandler<DeletePurchaseByCustomerRequest, Unit>
{
    private readonly Data _data;
    private readonly CommonExceptions _exceptions;

    public DeletePurchaseByCustomerHandler(Data data, CommonExceptions exceptions)
    {
        _data = data;
        _exceptions = exceptions;
    }

    public Task<Unit> Handle(DeletePurchaseByCustomerRequest request, CancellationToken cancellationToken)
    {
        var purchase = _data.purchases.FirstOrDefault(s => s.CustomerId == request.CustomerId);

        if (purchase == null)
        {
            _exceptions.ItemNotExists();
        }

        _data.purchases.Remove(purchase!);
        return Task.FromResult(Unit.Value);
    }
}
