using BackendTest.Application.Requests.Person;
using BackendTest.Application.Repositories;
using BackendTest.Application.Responses.Person;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Person;

public class AddPersonHandler : IRequestHandler<AddPersonRequest, AddPersonResponse>
{
    private readonly IPersonRepository _personRepository;

    public AddPersonHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public Task<AddPersonResponse> Handle(AddPersonRequest request, CancellationToken cancellationToken)
    {
        if (_personRepository.Exists(request.Id, cancellationToken))
        {
            throw new DuplicateException();
        }

        var person = new Model.Person(
            request.Id,
            request.Firstname,
            request.Lastname,
            request.YearOfBirth
        );

        _personRepository.Add(person, cancellationToken);
        return Task.FromResult(new AddPersonResponse(person));
    }
}
