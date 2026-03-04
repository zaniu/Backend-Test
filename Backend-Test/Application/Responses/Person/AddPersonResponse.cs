namespace BackendTest.Application.Responses.Person;

public record AddPersonResponse(int Id, string Firstname, string Lastname, int YearOfBirth)
{
    public AddPersonResponse(Model.Person person)
        : this(person.Id, person.Firstname, person.Lastname, person.YearOfBirth)
    {
    }
}
