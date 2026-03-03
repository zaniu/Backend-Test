namespace BackendTest.Application.Responses.Product;

public class GetProductByIdResponse
{
    public int Id { get; init; }

    public string Name { get; init; }

    public string Type { get; init; }

    public decimal Price { get; init; }

    public GetProductByIdResponse(Model.Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Type = product.Type;
        Price = product.Price;
    }
}
