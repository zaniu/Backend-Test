using BackendTest.Application.Requests.Product;
using BackendTest.Application.Repositories;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Product;

public class DeleteProductHandler : IRequestHandler<DeleteProductRequest, Unit>
{
    private readonly IProductRepository _productRepository;
    private readonly IPurchaseRepository _purchaseRepository;

    public DeleteProductHandler(IProductRepository productRepository, IPurchaseRepository purchaseRepository)
    {
        _productRepository = productRepository;
        _purchaseRepository = purchaseRepository;
    }

    public Task<Unit> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        if (_productRepository.Exists(request.Id, cancellationToken))
        {
            if (_purchaseRepository.ExistsByProductId(request.Id, cancellationToken))
            {
                throw new DomainModelException("Cannot delete product with existing purchases");
            }

            _productRepository.DeleteById(request.Id, cancellationToken);
        }
        else
        {
            throw new NotFoundException();
        }

        return Unit.Task;
    }
}
