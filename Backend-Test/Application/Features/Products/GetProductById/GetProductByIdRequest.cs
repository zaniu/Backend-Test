using MediatR;

namespace BackendTest.Application.Features.Products.GetProductById;

public record GetProductByIdRequest(int Id) : IRequest<GetProductByIdResponse>;
