using MediatR;

namespace BackendTest.Application.Features.Products.AddProduct;

public record AddProductRequest(int Id, string Name, string Type, decimal Price) : IRequest<AddProductResponse>;
