using BackendTest.Application.Requests.Person;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Person;

public class DeletePersonHandler : IRequestHandler<DeletePersonRequest, Unit>
{
    private readonly Data _data;
    private readonly HelperUtils _helper;

    public DeletePersonHandler(Data data, HelperUtils helper)
    {
        _data = data;
        _helper = helper;
    }

    public Task<Unit> Handle(DeletePersonRequest request, CancellationToken cancellationToken)
    {
        if (_helper.PersonExists(request.Id))
        {
            _data.persons.Remove(_data.persons.First(s => s.Id == request.Id));
        }
        else
        {
            throw new NotFoundException();
        }

        return Unit.Task;
    }
}
