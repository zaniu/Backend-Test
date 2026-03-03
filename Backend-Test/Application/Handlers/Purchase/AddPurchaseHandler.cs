using BackendTest.Application.Requests.Purchase;
using BackendTest.Application.Responses.Purchase;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Purchase;

public class AddPurchaseHandler : IRequestHandler<AddPurchaseRequest, AddPurchaseResponse>
{
    private readonly Data _data;

    public AddPurchaseHandler(Data data)
    {
        _data = data;
    }

    public Task<AddPurchaseResponse> Handle(AddPurchaseRequest request, CancellationToken cancellationToken)
    {
        if (_data.purchases.Any(p => p.Id == request.Id))
        {
            throw new DuplicateException();
        }

        var purchase = new Model.Purchase(request.Id, request.CustomerId, request.ProductsIds);
        _data.purchases.Add(purchase);

        return Task.FromResult(new AddPurchaseResponse(purchase));
    }
}
