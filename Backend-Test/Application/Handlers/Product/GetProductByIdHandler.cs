using BackendTest.Application.Requests.Product;
using BackendTest.Application.Repositories;
using BackendTest.Application.Responses.Product;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Product;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdRequest, GetProductByIdResponse>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<GetProductByIdResponse> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
    {
        var product = _productRepository.GetById(request.Id, cancellationToken);
        if (product == null)
        {
            throw new NotFoundException();
        }

        return Task.FromResult(new GetProductByIdResponse(product));
    }
}
