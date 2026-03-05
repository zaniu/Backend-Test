using MediatR;

namespace BackendTest.Application.Features.Persons.GetAllPersons;

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
        return new GetAllPersonsResponse(persons.Select(p => new GetAllPersonsResponse.PersonItem(p.Id, p.Firstname, p.Lastname, p.YearOfBirth)).ToList());
    }
}
