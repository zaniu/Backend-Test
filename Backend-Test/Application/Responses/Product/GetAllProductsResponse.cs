namespace BackendTest.Application.Responses.Product;

public record GetAllProductsResponse(IReadOnlyCollection<GetProductByIdResponse> Products)
{
    public GetAllProductsResponse(List<Model.Product> products)
        : this(products.Select(product => new GetProductByIdResponse(product)).ToList())
    {
    }
}
