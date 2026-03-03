using BackendTest.Application.Requests.Purchase;
using BackendTest.Application.Responses.Purchase;
using MediatR;

namespace BackendTest.Application.Handlers.Purchase;

public class AddPurchaseHandler : IRequestHandler<AddPurchaseRequest, AddPurchaseResponse>
{
    private readonly Data _data;
    private readonly CommonExceptions _exceptions;

    public AddPurchaseHandler(Data data, CommonExceptions exceptions)
    {
        _data = data;
        _exceptions = exceptions;
    }

    public Task<AddPurchaseResponse> Handle(AddPurchaseRequest request, CancellationToken cancellationToken)
    {
        if (_data.purchases.Any(p => p.Id == request.Id))
        {
            _exceptions.ItemAlreadyExists();
        }

        var purchase = new Model.Purchase(request.Id, request.CustomerId, request.ProductsIds);
        _data.purchases.Add(purchase);

        return Task.FromResult(new AddPurchaseResponse(purchase));
    }
}
