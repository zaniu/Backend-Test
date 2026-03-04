using BackendTest.Application.Responses.Product;
using MediatR;

namespace BackendTest.Application.Requests.Product;

public record GetAllProductsRequest : IRequest<GetAllProductsResponse>;
