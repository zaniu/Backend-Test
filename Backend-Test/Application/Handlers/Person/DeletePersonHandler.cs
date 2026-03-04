using BackendTest.Application.Requests.Person;
using BackendTest.Application.Repositories;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Person;

public class DeletePersonHandler : IRequestHandler<DeletePersonRequest, Unit>
{
    private readonly IPersonRepository _personRepository;
    private readonly IPurchaseRepository _purchaseRepository;

    public DeletePersonHandler(IPersonRepository personRepository, IPurchaseRepository purchaseRepository)
    {
        _personRepository = personRepository;
        _purchaseRepository = purchaseRepository;
    }

    public Task<Unit> Handle(DeletePersonRequest request, CancellationToken cancellationToken)
    {
        if (_personRepository.Exists(request.Id, cancellationToken))
        {
            if (_purchaseRepository.ExistsByCustomerId(request.Id, cancellationToken))
            {
                throw new DomainModelException("Cannot delete person with existing purchases");
            }

            _personRepository.DeleteById(request.Id, cancellationToken);
        }
        else
        {
            throw new NotFoundException();
        }

        return Unit.Task;
    }
}
