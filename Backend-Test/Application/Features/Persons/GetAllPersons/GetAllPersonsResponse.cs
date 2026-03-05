using BackendTest.Contracts;

namespace BackendTest.Application.Features.Persons.GetAllPersons;

public record GetAllPersonsResponse(IReadOnlyCollection<GetAllPersonsResponse.PersonItem> Value) : CollectionResponse<GetAllPersonsResponse.PersonItem>(Value)
{
    public record PersonItem(int Id, string Firstname, string Lastname, int YearOfBirth);
}
