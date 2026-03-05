using MediatR;

namespace BackendTest.Application.Features.Persons.AddPerson;

public record AddPersonRequest(int Id, string Firstname, string Lastname, int YearOfBirth) : IRequest<AddPersonResponse>;
