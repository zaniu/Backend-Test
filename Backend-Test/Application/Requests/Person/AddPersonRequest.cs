using MediatR;
using BackendTest.Application.Responses.Person;

namespace BackendTest.Application.Requests.Person;

public class AddPersonRequest : IRequest<AddPersonResponse>
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public int YearOfBirth { get; set; }

    public AddPersonRequest(int id, string firstname, string lastname, int yearOfBirth)
    {
        Id = id;
        Firstname = firstname;
        Lastname = lastname;
        YearOfBirth = yearOfBirth;
    }
}
