using BackendTest.Application.Requests.Product;
using BackendTest.Application.Repositories;
using BackendTest.Application.Responses.Product;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Product;

public class AddProductHandler : IRequestHandler<AddProductRequest, AddProductResponse>
{
    private readonly IProductRepository _productRepository;

    public AddProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<AddProductResponse> Handle(AddProductRequest request, CancellationToken cancellationToken)
    {
        var product = new Model.Product(
            request.Id,
            request.Name,
            request.Type,
            request.Price
        );

        var persistedProduct = await _productRepository.TryAdd(product, cancellationToken);
        if (persistedProduct == null)
        {
            throw new DuplicateException();
        }

        return new AddProductResponse(persistedProduct);
    }
}
