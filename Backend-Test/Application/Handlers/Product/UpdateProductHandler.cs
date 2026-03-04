using BackendTest.Application.Requests.Product;
using BackendTest.Application.Repositories;
using BackendTest.Application.Responses.Product;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Product;

public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, UpdateProductResponse>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<UpdateProductResponse> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var updatedProduct = new Model.Product(request.Id, request.Name, request.Type, request.Price);
        var persistedProduct = await _productRepository.TryUpdate(updatedProduct, cancellationToken);

        if (persistedProduct == null)
        {
            throw new NotFoundException();
        }

        return new UpdateProductResponse(persistedProduct);
    }
}
