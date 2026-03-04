using System.Text.Json.Serialization;
using BackendTest.Application.Responses.Person;
using MediatR;

namespace BackendTest.Application.Requests.Person;

public record UpdatePersonRequest(string Firstname, string Lastname, int YearOfBirth) : IRequest<UpdatePersonResponse>
{
    [JsonIgnore]
    public int Id { get; init; }
}
