using BackendTest.Application.Requests.Person;
using BackendTest.Application.Responses.Person;
using MediatR;

namespace BackendTest.Application.Handlers.Person;

public class GetPersonByIdHandler : IRequestHandler<GetPersonByIdRequest, GetPersonByIdResponse>
{
    private readonly Data _data;
    private readonly HelperUtils _helper;
    private readonly CommonExceptions _exceptions;

    public GetPersonByIdHandler(Data data, HelperUtils helper, CommonExceptions exceptions)
    {
        _data = data;
        _helper = helper;
        _exceptions = exceptions;
    }

    public Task<GetPersonByIdResponse> Handle(GetPersonByIdRequest request, CancellationToken cancellationToken)
    {
        if (!_helper.PersonExists(request.Id))
        {
            _exceptions.ItemNotExists();
            return Task.FromResult<GetPersonByIdResponse>(null);
        }

        var person = _data.persons.First(s => s.Id == request.Id);
        return Task.FromResult(new GetPersonByIdResponse(person));
    }
}
