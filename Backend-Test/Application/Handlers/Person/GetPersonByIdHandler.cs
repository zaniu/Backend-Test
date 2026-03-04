using BackendTest.Application.Requests.Person;
using BackendTest.Application.Repositories;
using BackendTest.Application.Responses.Person;
using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Handlers.Person;

public class GetPersonByIdHandler : IRequestHandler<GetPersonByIdRequest, GetPersonByIdResponse>
{
    private readonly IPersonRepository _personRepository;

    public GetPersonByIdHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public Task<GetPersonByIdResponse> Handle(GetPersonByIdRequest request, CancellationToken cancellationToken)
    {
        var person = _personRepository.GetById(request.Id, cancellationToken);
        if (person == null)
        {
            throw new NotFoundException();
        }

        return Task.FromResult(new GetPersonByIdResponse(person));
    }
}
