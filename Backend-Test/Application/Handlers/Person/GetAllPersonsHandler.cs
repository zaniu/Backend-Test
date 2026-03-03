using BackendTest.Application.Requests.Person;
using BackendTest.Application.Responses.Person;
using MediatR;

namespace BackendTest.Application.Handlers.Person;

public class GetAllPersonsHandler : IRequestHandler<GetAllPersonsRequest, GetAllPersonsResponse>
{
    private readonly Data _data;

    public GetAllPersonsHandler(Data data)
    {
        _data = data;
    }

    public Task<GetAllPersonsResponse> Handle(GetAllPersonsRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new GetAllPersonsResponse(_data.persons));
    }
}
