using MediatR;

namespace BackendTest.Application.Requests.Person;

public record DeletePersonRequest(int Id) : IRequest<Unit>;
