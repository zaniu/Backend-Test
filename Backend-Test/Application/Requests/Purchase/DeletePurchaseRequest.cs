using MediatR;

namespace BackendTest.Application.Requests.Purchase;

public class DeletePurchaseRequest : IRequest<Unit>
{
    public int Id { get; set; }

    public DeletePurchaseRequest(int id)
    {
        Id = id;
    }
}
