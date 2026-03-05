using MediatR;

namespace BackendTest.Application.Features.Products.GetAllProducts;

public record GetAllProductsRequest : IRequest<GetAllProductsResponse>;
