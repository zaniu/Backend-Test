using BackendTest.Application.Responses.Purchase;
using MediatR;

namespace BackendTest.Application.Requests.Purchase;

public class AddPurchaseRequest : IRequest<AddPurchaseResponse>
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public List<int> ProductsIds { get; set; }

    public AddPurchaseRequest(int id, int customerId, List<int> productsIds)
    {
        Id = id;
        CustomerId = customerId;
        ProductsIds = productsIds;
    }
}
