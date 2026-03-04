using System.Text.Json.Serialization;
using BackendTest.Application.Responses.Product;
using MediatR;

namespace BackendTest.Application.Requests.Product;

public record UpdateProductRequest(string Name, string Type, decimal Price) : IRequest<UpdateProductResponse>
{
    [JsonIgnore]
    public int Id { get; init; }
}
