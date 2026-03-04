using BackendTest.Application.Requests.Person;
using BackendTest.Application.Repositories;
using BackendTest.Application.Responses.Person;
using MediatR;

namespace BackendTest.Application.Handlers.Person;

public class GetAllPersonsHandler : IRequestHandler<GetAllPersonsRequest, GetAllPersonsResponse>
{
    private readonly IPersonRepository _personRepository;

    public GetAllPersonsHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<GetAllPersonsResponse> Handle(GetAllPersonsRequest request, CancellationToken cancellationToken)
    {
        var persons = await _personRepository.GetAll(cancellationToken);
        return new GetAllPersonsResponse(persons);
    }
}
