using BackendTest.Contracts;

namespace BackendTest.Application.Features.Products.GetAllProducts;

public record GetAllProductsResponse(IReadOnlyCollection<GetAllProductsResponse.ProductItem> Value) : CollectionResponse<GetAllProductsResponse.ProductItem>(Value)
{
    public record ProductItem(int Id, string Name, string Type, decimal Price);
}
