namespace BackendTest.Application.Responses.Person;

public class GetAllPersonsResponse : List<GetPersonByIdResponse>
{
    public GetAllPersonsResponse(List<Model.Person> persons)
    {
        AddRange(persons.Select(person => new GetPersonByIdResponse(person)));
    }
}
