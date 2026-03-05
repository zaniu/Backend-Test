using MediatR;

namespace BackendTest.Application.Features.Products.DeleteProduct;

public record DeleteProductRequest(int Id) : IRequest<Unit>;
