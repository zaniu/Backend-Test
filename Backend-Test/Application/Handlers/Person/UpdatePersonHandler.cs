using BackendTest.Application.Requests.Person;
using BackendTest.Application.Repositories;
using BackendTest.Application.Responses.Person;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Person;

public class UpdatePersonHandler : IRequestHandler<UpdatePersonRequest, UpdatePersonResponse>
{
    private readonly IPersonRepository _personRepository;

    public UpdatePersonHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public Task<UpdatePersonResponse> Handle(UpdatePersonRequest request, CancellationToken cancellationToken)
    {
        var existingPerson = _personRepository.GetById(request.Id, cancellationToken);
        if (existingPerson == null)
        {
            throw new NotFoundException();
        }

        var updatedPerson = new Model.Person(request.Id, request.Firstname, request.Lastname, request.YearOfBirth);
        _personRepository.Update(updatedPerson, cancellationToken);

        return Task.FromResult(new UpdatePersonResponse(updatedPerson));
    }
}
