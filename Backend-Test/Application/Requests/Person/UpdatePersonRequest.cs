using System.Text.Json.Serialization;
using BackendTest.Application.Responses.Person;
using MediatR;

namespace BackendTest.Application.Requests.Person;

public class UpdatePersonRequest : IRequest<UpdatePersonResponse>
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public int YearOfBirth { get; set; }

    public UpdatePersonRequest(int id, string firstname, string lastname, int yearOfBirth)
    {
        Id = id;
        Firstname = firstname;
        Lastname = lastname;
        YearOfBirth = yearOfBirth;
    }
}
