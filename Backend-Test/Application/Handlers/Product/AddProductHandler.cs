using BackendTest.Application.Requests.Product;
using BackendTest.Application.Responses.Product;
using MediatR;

namespace BackendTest.Application.Handlers.Product;

public class AddProductHandler : IRequestHandler<AddProductRequest, AddProductResponse>
{
    private readonly Data _data;
    private readonly CommonExceptions _exceptions;

    public AddProductHandler(Data data, CommonExceptions exceptions)
    {
        _data = data;
        _exceptions = exceptions;
    }

    public Task<AddProductResponse> Handle(AddProductRequest request, CancellationToken cancellationToken)
    {
        if (_data.products.Any(p => p.Id == request.Id))
        {
            _exceptions.ItemAlreadyExists();
        }

        var product = new Model.Product(
            request.Id,
            request.Name,
            request.Type,
            request.Price
        );

        _data.products.Add(product);
        return Task.FromResult(new AddProductResponse(product));
    }
}
