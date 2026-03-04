using BackendTest.Application.Responses.Product;
using MediatR;

namespace BackendTest.Application.Requests.Product;

public record GetProductByIdRequest(int Id) : IRequest<GetProductByIdResponse>;
