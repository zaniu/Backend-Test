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

    public Task<AddProductResponse> Handle(AddProductRequest request, CancellationToken cancellationToken)
    {
        if (_productRepository.Exists(request.Id, cancellationToken))
        {
            throw new DuplicateException();
        }

        var product = new Model.Product(
            request.Id,
            request.Name,
            request.Type,
            request.Price
        );

        _productRepository.Add(product, cancellationToken);
        return Task.FromResult(new AddProductResponse(product));
    }
}
