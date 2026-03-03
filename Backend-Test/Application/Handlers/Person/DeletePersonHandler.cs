using BackendTest.Application.Requests.Person;
using MediatR;

namespace BackendTest.Application.Handlers.Person;

public class DeletePersonHandler : IRequestHandler<DeletePersonRequest, Unit>
{
    private readonly Data _data;
    private readonly HelperUtils _helper;
    private readonly CommonExceptions _exceptions;

    public DeletePersonHandler(Data data, HelperUtils helper, CommonExceptions exceptions)
    {
        _data = data;
        _helper = helper;
        _exceptions = exceptions;
    }

    public Task<Unit> Handle(DeletePersonRequest request, CancellationToken cancellationToken)
    {
        if (_helper.PersonExists(request.Id))
        {
            _data.persons.Remove(_data.persons.First(s => s.Id == request.Id));
        }
        else
        {
            _exceptions.ItemNotExists();
        }

        return Unit.Task;
    }
}
