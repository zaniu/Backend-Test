using MediatR;
using BackendTest.Application.Responses.Person;

namespace BackendTest.Application.Requests.Person;

public record AddPersonRequest(int Id, string Firstname, string Lastname, int YearOfBirth) : IRequest<AddPersonResponse>;
