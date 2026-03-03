namespace BackendTest.Application.Responses.Person;

public class DeletePersonResponse
{
    public List<GetPersonByIdResponse> Persons { get; init; }

    public DeletePersonResponse(List<Model.Person> persons)
    {
        Persons = persons.Select(person => new GetPersonByIdResponse(person)).ToList();
    }
}
