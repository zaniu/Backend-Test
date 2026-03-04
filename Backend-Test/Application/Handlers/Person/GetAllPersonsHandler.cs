using BackendTest.Application.Requests.Person;
using BackendTest.Application.Repositories;
using BackendTest.Application.Responses.Person;
using MediatR;

namespace BackendTest.Application.Handlers.Person;

public class GetAllPersonsHandler : IRequestHandler<GetAllPersonsRequest, GetAllPersonsResponse>
{
    private readonly IPersonRepository _personRepository;

    public GetAllPersonsHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public Task<GetAllPersonsResponse> Handle(GetAllPersonsRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new GetAllPersonsResponse(_personRepository.GetAll(cancellationToken)));
    }
}
