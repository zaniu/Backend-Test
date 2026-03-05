using BackendTest.Application.Features.Purchases;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Features.Persons.DeletePerson;

public class DeletePersonHandler : IRequestHandler<DeletePersonRequest, Unit>
{
    private readonly IPersonRepository _personRepository;
    private readonly IPurchaseRepository _purchaseRepository;

    public DeletePersonHandler(IPersonRepository personRepository, IPurchaseRepository purchaseRepository)
    {
        _personRepository = personRepository;
        _purchaseRepository = purchaseRepository;
    }

    public async Task<Unit> Handle(DeletePersonRequest request, CancellationToken cancellationToken)
    {
        if (await _purchaseRepository.ExistsByCustomerId(request.Id, cancellationToken))
        {
            throw new DomainModelException("Cannot delete person with existing purchases");
        }

        var wasDeleted = await _personRepository.TryDeleteById(request.Id, cancellationToken);
        if (!wasDeleted)
        {
            throw new NotFoundException();
        }

        return Unit.Value;
    }
}
