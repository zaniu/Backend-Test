using BackendTest.Application.Features.Purchases;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Features.Products.DeleteProduct;

public class DeleteProductHandler : IRequestHandler<DeleteProductRequest, Unit>
{
    private readonly IProductRepository _productRepository;
    private readonly IPurchaseRepository _purchaseRepository;

    public DeleteProductHandler(IProductRepository productRepository, IPurchaseRepository purchaseRepository)
    {
        _productRepository = productRepository;
        _purchaseRepository = purchaseRepository;
    }

    public async Task<Unit> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        if (await _purchaseRepository.ExistsByProductId(request.Id, cancellationToken))
        {
            throw new DomainModelException("Cannot delete product with existing purchases");
        }

        var wasDeleted = await _productRepository.TryDeleteById(request.Id, cancellationToken);
        if (!wasDeleted)
        {
            throw new NotFoundException();
        }

        return Unit.Value;
    }
}
