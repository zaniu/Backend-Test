using System.Text.Json.Serialization;
using BackendTest.Application.Responses.Product;
using MediatR;

namespace BackendTest.Application.Requests.Product;

public class UpdateProductRequest : IRequest<UpdateProductResponse>
{
    [JsonIgnore]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public decimal Price { get; set; }

    public UpdateProductRequest(int id, string name, string type, decimal price)
    {
        Id = id;
        Name = name;
        Type = type;
        Price = price;
    }
}
