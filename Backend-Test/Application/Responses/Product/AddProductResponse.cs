namespace BackendTest.Application.Responses.Product;

public record AddProductResponse(int Id, string Name, string Type, decimal Price)
{
    public AddProductResponse(Model.Product product)
        : this(product.Id, product.Name, product.Type, product.Price)
    {
    }
}
