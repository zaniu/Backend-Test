using BackendTest.Application.Requests.Purchase;
using BackendTest.Application.Repositories;
using BackendTest.Application.Responses.Purchase;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Purchase;

public class AddPurchaseHandler : IRequestHandler<AddPurchaseRequest, AddPurchaseResponse>
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IProductRepository _productRepository;

    public AddPurchaseHandler(
        IPurchaseRepository purchaseRepository,
        IPersonRepository personRepository,
        IProductRepository productRepository)
    {
        _purchaseRepository = purchaseRepository;
        _personRepository = personRepository;
        _productRepository = productRepository;
    }

    public async Task<AddPurchaseResponse> Handle(AddPurchaseRequest request, CancellationToken cancellationToken)
    {
        if (!await _personRepository.Exists(request.CustomerId, cancellationToken))
        {
            throw new DomainModelException("Customer does not exist");
        }

        if (!await _productRepository.ExistsAll(request.ProductsIds, cancellationToken))
        {
            throw new DomainModelException("One or more products do not exist");
        }

        var purchase = new Model.Purchase(request.Id, request.CustomerId, request.ProductsIds);
        var persistedPurchase = await _purchaseRepository.TryAdd(purchase, cancellationToken);
        if (persistedPurchase == null)
        {
            throw new DuplicateException();
        }

        return new AddPurchaseResponse(persistedPurchase);
    }
}
