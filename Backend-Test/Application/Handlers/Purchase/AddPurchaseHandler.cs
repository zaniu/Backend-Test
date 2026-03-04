using BackendTest.Application.Requests.Purchase;
using BackendTest.Application.Repositories;
using BackendTest.Application.Responses.Purchase;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Purchase;

public class AddPurchaseHandler : IRequestHandler<AddPurchaseRequest, AddPurchaseResponse>
{
    private readonly IPurchaseRepository _purchaseRepository;

    public AddPurchaseHandler(IPurchaseRepository purchaseRepository)
    {
        _purchaseRepository = purchaseRepository;
    }

    public Task<AddPurchaseResponse> Handle(AddPurchaseRequest request, CancellationToken cancellationToken)
    {
        if (_purchaseRepository.Exists(request.Id, cancellationToken))
        {
            throw new DuplicateException();
        }

        var purchase = new Model.Purchase(request.Id, request.CustomerId, request.ProductsIds);
        _purchaseRepository.Add(purchase, cancellationToken);

        return Task.FromResult(new AddPurchaseResponse(purchase));
    }
}
