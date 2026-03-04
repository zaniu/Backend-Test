using BackendTest.Application.Requests.Product;
using BackendTest.Application.Repositories;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Product;

public class DeleteProductHandler : IRequestHandler<DeleteProductRequest, Unit>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<Unit> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        if (_productRepository.Exists(request.Id, cancellationToken))
        {
            _productRepository.DeleteById(request.Id, cancellationToken);
        }
        else
        {
            throw new NotFoundException();
        }

        return Unit.Task;
    }
}
