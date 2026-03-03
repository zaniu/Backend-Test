using BackendTest.Application.Requests.Purchase;
using BackendTest.Application.Responses.Purchase;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Purchase;

public class GetPurchaseByCustomerIdHandler : IRequestHandler<GetPurchaseByCustomerIdRequest, GetPurchaseByCustomerIdResponse>
{
    private readonly Data _data;

    public GetPurchaseByCustomerIdHandler(Data data)
    {
        _data = data;
    }

    public Task<GetPurchaseByCustomerIdResponse> Handle(GetPurchaseByCustomerIdRequest request, CancellationToken cancellationToken)
    {
        var purchase = _data.purchases.FirstOrDefault(s => s.CustomerId == request.CustomerId);

        if (purchase == null)
        {
            throw new NotFoundException();
        }

        return Task.FromResult(new GetPurchaseByCustomerIdResponse(purchase!));
    }
}
