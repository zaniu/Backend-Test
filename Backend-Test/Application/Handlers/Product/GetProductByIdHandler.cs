using BackendTest.Application.Requests.Product;
using BackendTest.Application.Responses.Product;
using MediatR;

namespace BackendTest.Application.Handlers.Product;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdRequest, GetProductByIdResponse>
{
    private readonly Data _data;
    private readonly HelperUtils _helper;
    private readonly CommonExceptions _exceptions;

    public GetProductByIdHandler(Data data, HelperUtils helper, CommonExceptions exceptions)
    {
        _data = data;
        _helper = helper;
        _exceptions = exceptions;
    }

    public Task<GetProductByIdResponse> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
    {
        if (!_helper.ProductExists(request.Id))
        {
            _exceptions.ItemNotExists();
            return Task.FromResult<GetProductByIdResponse>(null);
        }

        var product = _data.products.First(s => s.Id == request.Id);
        return Task.FromResult(new GetProductByIdResponse(product));
    }
}
