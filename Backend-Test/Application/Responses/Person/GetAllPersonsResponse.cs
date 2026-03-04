namespace BackendTest.Application.Responses.Person;

public record GetAllPersonsResponse(IReadOnlyCollection<GetPersonByIdResponse> Persons)
{
    public GetAllPersonsResponse(List<Model.Person> persons)
        : this(persons.Select(person => new GetPersonByIdResponse(person)).ToList())
    {
    }
}