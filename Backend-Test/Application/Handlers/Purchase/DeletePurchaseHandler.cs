using BackendTest.Application.Requests.Purchase;
using MediatR;

namespace BackendTest.Application.Handlers.Purchase;

public class DeletePurchaseHandler : IRequestHandler<DeletePurchaseRequest, Unit>
{
    private readonly Data _data;
    private readonly HelperUtils _helper;
    private readonly CommonExceptions _exceptions;

    public DeletePurchaseHandler(Data data, HelperUtils helper, CommonExceptions exceptions)
    {
        _data = data;
        _helper = helper;
        _exceptions = exceptions;
    }

    public Task<Unit> Handle(DeletePurchaseRequest request, CancellationToken cancellationToken)
    {
        if (!_helper.PurchaseExists(request.Id))
        {
            _exceptions.ItemNotExists();
        }

        var purchase = _data.purchases.First(s => s.Id == request.Id);
        _data.purchases.Remove(purchase);
        return Task.FromResult(Unit.Value);
    }
}
