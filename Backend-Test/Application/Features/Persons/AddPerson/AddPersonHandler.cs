using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Features.Persons.AddPerson;

public class AddPersonHandler : IRequestHandler<AddPersonRequest, AddPersonResponse>
{
    private readonly IPersonRepository _personRepository;

    public AddPersonHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<AddPersonResponse> Handle(AddPersonRequest request, CancellationToken cancellationToken)
    {
        var person = new Model.Person(
            request.Id,
            request.Firstname,
            request.Lastname,
            request.YearOfBirth
        );

        var persistedPerson = await _personRepository.TryAdd(person, cancellationToken);
        if (persistedPerson == null)
        {
            throw new DuplicateException();
        }

        return new AddPersonResponse(persistedPerson.Id, persistedPerson.Firstname, persistedPerson.Lastname, persistedPerson.YearOfBirth);
    }
}
