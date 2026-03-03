using BackendTest.Application.Requests.Product;
using MediatR;

namespace BackendTest.Application.Handlers.Product;

public class DeleteProductHandler : IRequestHandler<DeleteProductRequest, Unit>
{
    private readonly Data _data;
    private readonly HelperUtils _helper;
    private readonly CommonExceptions _exceptions;

    public DeleteProductHandler(Data data, HelperUtils helper, CommonExceptions exceptions)
    {
        _data = data;
        _helper = helper;
        _exceptions = exceptions;
    }

    public Task<Unit> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        if (_helper.ProductExists(request.Id))
        {
            _data.products.Remove(_data.products.First(s => s.Id == request.Id));
        }
        else
        {
            _exceptions.ItemNotExists();
        }

        return Unit.Task;
    }
}
