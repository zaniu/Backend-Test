using BackendTest.Application.Requests.Product;
using BackendTest.Application.Responses.Product;
using MediatR;

namespace BackendTest.Application.Handlers.Product;

public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, UpdateProductResponse>
{
    private readonly Data _data;
    private readonly CommonExceptions _exceptions;
    private readonly HelperUtils _helper;

    public UpdateProductHandler(Data data, CommonExceptions exceptions, HelperUtils helper)
    {
        _data = data;
        _exceptions = exceptions;
        _helper = helper;
    }

    public Task<UpdateProductResponse> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        if (!_helper.ProductExists(request.Id))
        {
            _exceptions.ItemNotExists();
        }

        var existingProductIndex = _data.products.FindIndex(p => p.Id == request.Id);
        if (existingProductIndex != -1)
        {
            _data.products[existingProductIndex] = new Model.Product(request.Id, request.Name, request.Type, request.Price);
        }
        else
        {
            _exceptions.ItemNotExists();
        }

        return Task.FromResult(new UpdateProductResponse(_data.products[existingProductIndex]));
    }
}
