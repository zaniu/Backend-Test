using MediatR;

namespace BackendTest.Application.Features.Products.GetAllProducts;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsRequest, GetAllProductsResponse>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<GetAllProductsResponse> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAll(cancellationToken);
        return new GetAllProductsResponse(products.Select(p =>
            new GetAllProductsResponse.ProductItem(
                p.Id,
                p.Name,
                p.Type,
                p.Price))
            .ToList());
    }
}
