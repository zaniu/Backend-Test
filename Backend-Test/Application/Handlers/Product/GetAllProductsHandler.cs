using BackendTest.Application.Requests.Product;
using BackendTest.Application.Responses.Product;
using MediatR;

namespace BackendTest.Application.Handlers.Product;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsRequest, GetAllProductsResponse>
{
    private readonly Data _data;

    public GetAllProductsHandler(Data data)
    {
        _data = data;
    }

    public Task<GetAllProductsResponse> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new GetAllProductsResponse(_data.products));
    }
}
