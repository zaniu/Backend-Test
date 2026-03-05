using System.Text.Json.Serialization;
using MediatR;

namespace BackendTest.Application.Features.Persons.UpdatePerson;

public record UpdatePersonRequest(string Firstname, string Lastname, int YearOfBirth) : IRequest<UpdatePersonResponse>
{
    [JsonIgnore]
    public int Id { get; init; }
}
