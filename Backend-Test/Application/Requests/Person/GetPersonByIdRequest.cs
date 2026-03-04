using BackendTest.Application.Responses.Person;
using MediatR;

namespace BackendTest.Application.Requests.Person;

public record GetPersonByIdRequest(int Id) : IRequest<GetPersonByIdResponse>;
