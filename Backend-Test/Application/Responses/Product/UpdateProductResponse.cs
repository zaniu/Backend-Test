namespace BackendTest.Application.Responses.Product;

public record UpdateProductResponse(int Id, string Name, string Type, decimal Price)
{
    public UpdateProductResponse(Model.Product product)
        : this(product.Id, product.Name, product.Type, product.Price)
    {
    }
}
