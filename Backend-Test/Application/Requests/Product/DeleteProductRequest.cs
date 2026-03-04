using MediatR;

namespace BackendTest.Application.Requests.Product;

public record DeleteProductRequest(int Id) : IRequest<Unit>;
