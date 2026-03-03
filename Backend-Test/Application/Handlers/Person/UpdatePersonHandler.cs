using BackendTest.Application.Requests.Person;
using BackendTest.Application.Responses.Person;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Person;

public class UpdatePersonHandler : IRequestHandler<UpdatePersonRequest, UpdatePersonResponse>
{
    private readonly Data _data;
    private readonly HelperUtils _helper;

    public UpdatePersonHandler(Data data, HelperUtils helper)
    {
        _data = data;
        _helper = helper;
    }

    public Task<UpdatePersonResponse> Handle(UpdatePersonRequest request, CancellationToken cancellationToken)
    {
        if (!_helper.PersonExists(request.Id))
        {
            throw new NotFoundException();
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
            throw new NotFoundException();
        }

        return Task.FromResult(new UpdatePersonResponse(existingPerson));
    }
}
