using BackendTest.Application.Responses.Product;
using MediatR;

namespace BackendTest.Application.Requests.Product;

public record AddProductRequest(int Id, string Name, string Type, decimal Price) : IRequest<AddProductResponse>;
