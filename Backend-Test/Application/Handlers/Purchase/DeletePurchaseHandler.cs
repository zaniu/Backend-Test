using BackendTest.Application.Requests.Purchase;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Purchase;

public class DeletePurchaseHandler : IRequestHandler<DeletePurchaseRequest, Unit>
{
    private readonly Data _data;
    private readonly HelperUtils _helper;

    public DeletePurchaseHandler(Data data, HelperUtils helper)
    {
        _data = data;
        _helper = helper;
    }

    public Task<Unit> Handle(DeletePurchaseRequest request, CancellationToken cancellationToken)
    {
        if (!_helper.PurchaseExists(request.Id))
        {
            throw new NotFoundException();
        }

        var purchase = _data.purchases.First(s => s.Id == request.Id);
        _data.purchases.Remove(purchase);
        return Task.FromResult(Unit.Value);
    }
}
