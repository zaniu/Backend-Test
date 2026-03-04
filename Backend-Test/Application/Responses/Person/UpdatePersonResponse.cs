namespace BackendTest.Application.Responses.Person;

public record UpdatePersonResponse(int Id, string Firstname, string Lastname, int YearOfBirth)
{
    public UpdatePersonResponse(Model.Person person)
        : this(person.Id, person.Firstname, person.Lastname, person.YearOfBirth)
    {
    }
}
