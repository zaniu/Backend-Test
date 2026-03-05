using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Features.Persons.GetPersonById;

public class GetPersonByIdHandler : IRequestHandler<GetPersonByIdRequest, GetPersonByIdResponse>
{
    private readonly IPersonRepository _personRepository;

    public GetPersonByIdHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<GetPersonByIdResponse> Handle(GetPersonByIdRequest request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetById(request.Id, cancellationToken);
        if (person == null)
        {
            throw new NotFoundException();
        }

        return new GetPersonByIdResponse(person.Id, person.Firstname, person.Lastname, person.YearOfBirth);
    }
}
