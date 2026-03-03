using BackendTest.Application.Requests.Product;
using BackendTest.Application.Responses.Product;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Product;

public class AddProductHandler : IRequestHandler<AddProductRequest, AddProductResponse>
{
    private readonly Data _data;

    public AddProductHandler(Data data)
    {
        _data = data;
    }

    public Task<AddProductResponse> Handle(AddProductRequest request, CancellationToken cancellationToken)
    {
        if (_data.products.Any(p => p.Id == request.Id))
        {
            throw new DuplicateException();
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
