using BackendTest.Application.Requests.Product;
using BackendTest.Application.Responses.Product;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Product;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdRequest, GetProductByIdResponse>
{
    private readonly Data _data;
    private readonly HelperUtils _helper;

    public GetProductByIdHandler(Data data, HelperUtils helper)
    {
        _data = data;
        _helper = helper;
    }

    public Task<GetProductByIdResponse> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
    {
        if (!_helper.ProductExists(request.Id))
        {
            throw new NotFoundException();
        }

        var product = _data.products.First(s => s.Id == request.Id);
        return Task.FromResult(new GetProductByIdResponse(product));
    }
}
