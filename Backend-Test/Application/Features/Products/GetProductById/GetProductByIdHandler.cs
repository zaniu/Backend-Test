using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Features.Products.GetProductById;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdRequest, GetProductByIdResponse>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<GetProductByIdResponse> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(request.Id, cancellationToken);
        if (product == null)
        {
            throw new NotFoundException();
        }

        return new GetProductByIdResponse(product.Id, product.Name, product.Type, product.Price);
    }
}
