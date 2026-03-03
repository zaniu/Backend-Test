using BackendTest.Application.Responses.Product;
using MediatR;

namespace BackendTest.Application.Requests.Product;

public class GetProductByIdRequest : IRequest<GetProductByIdResponse>
{
    public int Id { get; set; }

    public GetProductByIdRequest(int id)
    {
        Id = id;
    }
}
