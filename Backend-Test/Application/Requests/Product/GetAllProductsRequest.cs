using BackendTest.Application.Responses.Product;
using MediatR;

namespace BackendTest.Application.Requests.Product;

public class GetAllProductsRequest : IRequest<GetAllProductsResponse>
{
}
