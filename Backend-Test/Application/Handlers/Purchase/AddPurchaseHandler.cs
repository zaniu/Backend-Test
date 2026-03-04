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

        var requestedProductIds = request.Items.Select(item => item.ProductId).Distinct().ToList();

        if (!await _productRepository.ExistsAll(requestedProductIds, cancellationToken))
        {
            throw new DomainModelException("One or more products do not exist");
        }

        var purchase = MapToDomain(request);
        var persistedPurchase = await _purchaseRepository.TryAdd(purchase, cancellationToken);
        if (persistedPurchase == null)
        {
            throw new DuplicateException();
        }

        return new AddPurchaseResponse(persistedPurchase);
    }

    private static Model.Purchase MapToDomain(AddPurchaseRequest request)
    {
        var items = request.Items
            .Select(item => new Model.PurchaseProductItem(item.ProductId, item.Count))
            .ToList();

        return new Model.Purchase(
            request.Id,
            request.CustomerId,
            items);
    }
}
