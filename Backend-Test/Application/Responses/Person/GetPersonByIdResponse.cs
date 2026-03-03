namespace BackendTest.Application.Responses.Person;

public class GetPersonByIdResponse
{
    public int Id { get; init; }

    public string Firstname { get; init; }

    public string Lastname { get; init; }

    public int YearOfBirth { get; init; }

    public GetPersonByIdResponse(Model.Person person)
    {
        Id = person.Id;
        Firstname = person.Firstname;
        Lastname = person.Lastname;
        YearOfBirth = person.YearOfBirth;
    }
}
