using MediatR;

namespace BackendTest.Application.Requests.Product;

public class DeleteProductRequest : IRequest<Unit>
{
    public int Id { get; set; }

    public DeleteProductRequest(int id)
    {
        Id = id;
    }
}
