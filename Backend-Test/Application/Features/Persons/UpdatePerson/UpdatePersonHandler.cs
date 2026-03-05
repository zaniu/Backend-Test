using BackendTest.Exceptions;
using MediatR;
namespace BackendTest.Application.Features.Persons.UpdatePerson;

public class UpdatePersonHandler : IRequestHandler<UpdatePersonRequest, UpdatePersonResponse>
{
    private readonly IPersonRepository _personRepository;

    public UpdatePersonHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<UpdatePersonResponse> Handle(UpdatePersonRequest request, CancellationToken cancellationToken)
    {
        var updatedPerson = new Model.Person(request.Id, request.Firstname, request.Lastname, request.YearOfBirth);
        var persistedPerson = await _personRepository.TryUpdate(updatedPerson, cancellationToken);

        if (persistedPerson == null)
        {
            throw new NotFoundException();
        }

        return new UpdatePersonResponse(persistedPerson.Id, persistedPerson.Firstname, persistedPerson.Lastname, persistedPerson.YearOfBirth);
    }
}
