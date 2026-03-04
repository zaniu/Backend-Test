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

    public Task<UpdateProductResponse> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var existingProduct = _productRepository.GetById(request.Id, cancellationToken);
        if (existingProduct == null)
        {
            throw new NotFoundException();
        }

        var updatedProduct = new Model.Product(request.Id, request.Name, request.Type, request.Price);
        _productRepository.Update(updatedProduct, cancellationToken);

        return Task.FromResult(new UpdateProductResponse(updatedProduct));
    }
}
