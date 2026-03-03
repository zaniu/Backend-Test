using BackendTest.Application.Requests.Person;
using BackendTest.Application.Responses.Person;
using MediatR;

namespace BackendTest.Application.Handlers.Person;

public class UpdatePersonHandler : IRequestHandler<UpdatePersonRequest, UpdatePersonResponse>
{
    private readonly Data _data;
    private readonly CommonExceptions _exceptions;

    private readonly HelperUtils _helper;

    public UpdatePersonHandler(Data data, CommonExceptions exceptions, HelperUtils helper)
    {
        _data = data;
        _exceptions = exceptions;
        _helper = helper;
    }

    public Task<UpdatePersonResponse> Handle(UpdatePersonRequest request, CancellationToken cancellationToken)
    {
        //TODO fluent validation on the controller
        if (request.YearOfBirth > DateTime.UtcNow.Year)
        {
            throw new Exception("Customer can not be born after current year");
        }

        if (!_helper.PersonExists(request.Id))
        {
            _exceptions.ItemNotExists();
        }

        var existingPerson = _data.persons.Single(p => p.Id == request.Id);
        if (existingPerson != null)
        {
            existingPerson.Firstname = request.Firstname;
            existingPerson.Lastname = request.Lastname;
            existingPerson.YearOfBirth = request.YearOfBirth;
        }
        else 
        {
            _exceptions.ItemNotExists();
        }

        return Task.FromResult(new UpdatePersonResponse(existingPerson));
    }
}
