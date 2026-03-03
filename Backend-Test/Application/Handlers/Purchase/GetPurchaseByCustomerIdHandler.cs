using BackendTest.Application.Requests.Purchase;
using BackendTest.Application.Responses.Purchase;
using MediatR;

namespace BackendTest.Application.Handlers.Purchase;

public class GetPurchaseByCustomerIdHandler : IRequestHandler<GetPurchaseByCustomerIdRequest, GetPurchaseByCustomerIdResponse>
{
    private readonly Data _data;
    private readonly CommonExceptions _exceptions;

    public GetPurchaseByCustomerIdHandler(Data data, CommonExceptions exceptions)
    {
        _data = data;
        _exceptions = exceptions;
    }

    public Task<GetPurchaseByCustomerIdResponse> Handle(GetPurchaseByCustomerIdRequest request, CancellationToken cancellationToken)
    {
        var purchase = _data.purchases.FirstOrDefault(s => s.CustomerId == request.CustomerId);

        if (purchase == null)
        {
            _exceptions.ItemNotExists();
        }

        return Task.FromResult(new GetPurchaseByCustomerIdResponse(purchase!));
    }
}
