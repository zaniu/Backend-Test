using System.Text.Json.Serialization;
using MediatR;

namespace BackendTest.Application.Features.Products.UpdateProduct;

public record UpdateProductRequest(string Name, string Type, decimal Price) : IRequest<UpdateProductResponse>
{
    [JsonIgnore]
    public int Id { get; init; }
}
