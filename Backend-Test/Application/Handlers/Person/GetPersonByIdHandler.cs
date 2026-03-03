using BackendTest.Application.Requests.Person;
using BackendTest.Application.Responses.Person;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Person;

public class GetPersonByIdHandler : IRequestHandler<GetPersonByIdRequest, GetPersonByIdResponse>
{
    private readonly Data _data;
    private readonly HelperUtils _helper;

    public GetPersonByIdHandler(Data data, HelperUtils helper)
    {
        _data = data;
        _helper = helper;
    }

    public Task<GetPersonByIdResponse> Handle(GetPersonByIdRequest request, CancellationToken cancellationToken)
    {
        if (!_helper.PersonExists(request.Id))
        {
            throw new NotFoundException();
        }

        var person = _data.persons.First(s => s.Id == request.Id);
        return Task.FromResult(new GetPersonByIdResponse(person));
    }
}
