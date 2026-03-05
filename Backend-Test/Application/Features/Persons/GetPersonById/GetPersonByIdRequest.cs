using MediatR;

namespace BackendTest.Application.Features.Persons.GetPersonById;

public record GetPersonByIdRequest(int Id) : IRequest<GetPersonByIdResponse>;
