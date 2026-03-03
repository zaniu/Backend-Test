using BackendTest.Application.Responses.Purchase;
using MediatR;

namespace BackendTest.Application.Requests.Purchase;

public class GetPurchaseByCustomerIdRequest : IRequest<GetPurchaseByCustomerIdResponse>
{
    public int CustomerId { get; set; }

    public GetPurchaseByCustomerIdRequest(int customerId)
    {
        CustomerId = customerId;
    }
}
