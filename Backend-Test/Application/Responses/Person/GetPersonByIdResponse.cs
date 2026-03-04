namespace BackendTest.Application.Responses.Person;

public record GetPersonByIdResponse(int Id, string Firstname, string Lastname, int YearOfBirth)
{
    public GetPersonByIdResponse(Model.Person person)
        : this(person.Id, person.Firstname, person.Lastname, person.YearOfBirth)
    {
    }
}
