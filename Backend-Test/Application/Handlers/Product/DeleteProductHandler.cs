using BackendTest.Application.Requests.Product;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Product;

public class DeleteProductHandler : IRequestHandler<DeleteProductRequest, Unit>
{
    private readonly Data _data;
    private readonly HelperUtils _helper;

    public DeleteProductHandler(Data data, HelperUtils helper)
    {
        _data = data;
        _helper = helper;
    }

    public Task<Unit> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        if (_helper.ProductExists(request.Id))
        {
            _data.products.Remove(_data.products.First(s => s.Id == request.Id));
        }
        else
        {
            throw new NotFoundException();
        }

        return Unit.Task;
    }
}
