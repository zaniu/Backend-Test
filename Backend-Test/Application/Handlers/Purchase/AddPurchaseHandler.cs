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

    public Task<AddPurchaseResponse> Handle(AddPurchaseRequest request, CancellationToken cancellationToken)
    {
        if (_purchaseRepository.Exists(request.Id, cancellationToken))
        {
            throw new DuplicateException();
        }

        if (!_personRepository.Exists(request.CustomerId, cancellationToken))
        {
            throw new DomainModelException("Customer does not exist");
        }

        if (request.ProductsIds.Any(productId => !_productRepository.Exists(productId, cancellationToken)))
        {
            throw new DomainModelException("One or more products do not exist");
        }

        var purchase = new Model.Purchase(request.Id, request.CustomerId, request.ProductsIds);
        _purchaseRepository.Add(purchase, cancellationToken);

        return Task.FromResult(new AddPurchaseResponse(purchase));
    }
}
