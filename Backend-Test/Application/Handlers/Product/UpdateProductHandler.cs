using BackendTest.Application.Requests.Product;
using BackendTest.Application.Responses.Product;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Product;

public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, UpdateProductResponse>
{
    private readonly Data _data;
    private readonly HelperUtils _helper;

    public UpdateProductHandler(Data data, HelperUtils helper)
    {
        _data = data;
        _helper = helper;
    }

    public Task<UpdateProductResponse> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        if (!_helper.ProductExists(request.Id))
        {
            throw new NotFoundException();
        }

        var existingProductIndex = _data.products.FindIndex(p => p.Id == request.Id);
        if (existingProductIndex != -1)
        {
            _data.products[existingProductIndex] = new Model.Product(request.Id, request.Name, request.Type, request.Price);
        }
        else
        {
            throw new NotFoundException();
        }

        return Task.FromResult(new UpdateProductResponse(_data.products[existingProductIndex]));
    }
}
