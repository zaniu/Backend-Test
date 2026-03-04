namespace BackendTest.Application.Responses.Product;

public record GetProductByIdResponse(int Id, string Name, string Type, decimal Price)
{
    public GetProductByIdResponse(Model.Product product)
        : this(product.Id, product.Name, product.Type, product.Price)
    {
    }
}
