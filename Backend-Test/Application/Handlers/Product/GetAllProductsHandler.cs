using BackendTest.Application.Requests.Product;
using BackendTest.Application.Repositories;
using BackendTest.Application.Responses.Product;
using MediatR;

namespace BackendTest.Application.Handlers.Product;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsRequest, GetAllProductsResponse>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<GetAllProductsResponse> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new GetAllProductsResponse(_productRepository.GetAll(cancellationToken)));
    }
}
