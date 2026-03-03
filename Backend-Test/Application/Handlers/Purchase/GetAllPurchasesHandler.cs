using BackendTest.Application.Requests.Purchase;
using BackendTest.Application.Responses.Purchase;
using MediatR;

namespace BackendTest.Application.Handlers.Purchase;

public class GetAllPurchasesHandler : IRequestHandler<GetAllPurchasesRequest, GetAllPurchasesResponse>
{
    private readonly Data _data;

    public GetAllPurchasesHandler(Data data)
    {
        _data = data;
    }

    public Task<GetAllPurchasesResponse> Handle(GetAllPurchasesRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new GetAllPurchasesResponse(_data.purchases));
    }
}
