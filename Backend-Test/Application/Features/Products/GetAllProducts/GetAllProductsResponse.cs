namespace BackendTest.Application.Features.Products.GetAllProducts;

public record GetAllProductsResponse(IReadOnlyCollection<GetAllProductsResponse.ProductItem> Products)
{
    public record ProductItem(int Id, string Name, string Type, decimal Price);
}
