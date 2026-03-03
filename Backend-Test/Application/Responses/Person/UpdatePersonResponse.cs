namespace BackendTest.Application.Responses.Person;

public class UpdatePersonResponse
{
    public int Id { get; init; }
    public string Firstname { get; init; }
    public string Lastname { get; init; }
    public int YearOfBirth { get; init; }

    public UpdatePersonResponse()
    {
    }

    public UpdatePersonResponse(Model.Person person)
    {
        Id = person.Id;
        Firstname = person.Firstname;
        Lastname = person.Lastname;
        YearOfBirth = person.YearOfBirth;
    }
}
