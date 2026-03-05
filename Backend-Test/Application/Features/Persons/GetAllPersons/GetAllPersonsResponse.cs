namespace BackendTest.Application.Features.Persons.GetAllPersons;

public record GetAllPersonsResponse(IReadOnlyCollection<GetAllPersonsResponse.PersonItem> Persons)
{
    public record PersonItem(int Id, string Firstname, string Lastname, int YearOfBirth);
}
