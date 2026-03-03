using MediatR;

namespace BackendTest.Application.Requests.Purchase;

public class DeletePurchaseByCustomerRequest : IRequest<Unit>
{
    public int CustomerId { get; set; }

    public DeletePurchaseByCustomerRequest(int customerId)
    {
        CustomerId = customerId;
    }
}
