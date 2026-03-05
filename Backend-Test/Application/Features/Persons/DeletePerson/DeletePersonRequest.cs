using MediatR;

namespace BackendTest.Application.Features.Persons.DeletePerson;

public record DeletePersonRequest(int Id) : IRequest<Unit>;
