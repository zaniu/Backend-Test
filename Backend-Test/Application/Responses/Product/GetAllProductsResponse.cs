namespace BackendTest.Application.Responses.Product;

public class GetAllProductsResponse : List<GetProductByIdResponse>
{
    public GetAllProductsResponse()
    {
    }

    public GetAllProductsResponse(List<Model.Product> products)
    {
        AddRange(products.Select(product => new GetProductByIdResponse(product)));
    }
}
