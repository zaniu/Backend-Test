using BackendTest.Application.Responses.Product;
using MediatR;

namespace BackendTest.Application.Requests.Product;

public class AddProductRequest : IRequest<AddProductResponse>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public decimal Price { get; set; }

    public AddProductRequest(int id, string name, string type, decimal price)
    {
        Id = id;
        Name = name;
        Type = type;
        Price = price;
    }
}
