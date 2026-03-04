using BackendTest.Application.Requests.Person;
using BackendTest.Application.Repositories;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Person;

public class DeletePersonHandler : IRequestHandler<DeletePersonRequest, Unit>
{
    private readonly IPersonRepository _personRepository;

    public DeletePersonHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public Task<Unit> Handle(DeletePersonRequest request, CancellationToken cancellationToken)
    {
        if (_personRepository.Exists(request.Id, cancellationToken))
        {
            _personRepository.DeleteById(request.Id, cancellationToken);
        }
        else
        {
            throw new NotFoundException();
        }

        return Unit.Task;
    }
}
