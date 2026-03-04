using BackendTest.Application.Repositories;

namespace BackendTest.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly Data _data;

    public PersonRepository(Data data)
    {
        _data = data;
    }

    public bool Exists(int id, CancellationToken cancellationToken)
    {
        return _data.persons.Any(person => person.Id == id);
    }

    public List<Model.Person> GetAll(CancellationToken cancellationToken)
    {
        return _data.persons;
    }

    public Model.Person GetById(int id, CancellationToken cancellationToken)
    {
        return _data.persons.FirstOrDefault(person => person.Id == id);
    }

    public void Add(Model.Person person, CancellationToken cancellationToken)
    {
        _data.persons.Add(person);
    }

    public void Update(Model.Person person, CancellationToken cancellationToken)
    {
        var existingPersonIndex = _data.persons.FindIndex(p => p.Id == person.Id);
        if (existingPersonIndex != -1)
        {
            _data.persons[existingPersonIndex] = person;
        }
    }

    public void DeleteById(int id, CancellationToken cancellationToken)
    {
        var person = GetById(id, cancellationToken);
        if (person != null)
        {
            _data.persons.Remove(person);
        }
    }
}