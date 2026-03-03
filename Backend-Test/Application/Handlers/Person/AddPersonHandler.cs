using BackendTest.Application.Requests.Person;
using BackendTest.Application.Responses.Person;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Person;

public class AddPersonHandler : IRequestHandler<AddPersonRequest, AddPersonResponse>
{
    private readonly Data _data;

    public AddPersonHandler(Data data)
    {
        _data = data;
    }

    public Task<AddPersonResponse> Handle(AddPersonRequest request, CancellationToken cancellationToken)
    {
        if(_data.persons.Any(p => p.Id == request.Id))
        {
            throw new DuplicateException();
        }

        var person = new Model.Person(
            request.Id,
            request.Firstname,
            request.Lastname,
            request.YearOfBirth
        );

        _data.persons.Add(person);
        return Task.FromResult(new AddPersonResponse(person));
    }
}
