using BackendTest.Application.Features.Persons;

namespace BackendTest.Infrastructure.Features.Persons;

public class PersonRepository : IPersonRepository
{
    private readonly Data _data;

    public PersonRepository(Data data)
    {
        _data = data;
    }

    public Task<bool> Exists(int id, CancellationToken cancellationToken)
    {
        return Task.FromResult(_data.persons.Any(person => person.Id == id));
    }

    public Task<List<Model.Person>> GetAll(CancellationToken cancellationToken)
    {
        return Task.FromResult(_data.persons.ToList());
    }

    public Task<Model.Person> GetById(int id, CancellationToken cancellationToken)
    {
        return Task.FromResult(_data.persons.FirstOrDefault(person => person.Id == id));
    }

    public Task<Model.Person> TryAdd(Model.Person person, CancellationToken cancellationToken)
    {
        if (_data.persons.Any(existingPerson => existingPerson.Id == person.Id))
        {
            return Task.FromResult<Model.Person>(null);
        }

        _data.persons.Add(person);
        return Task.FromResult(person);
    }

    public Task<Model.Person> TryUpdate(Model.Person person, CancellationToken cancellationToken)
    {
        var existingPersonIndex = _data.persons.FindIndex(p => p.Id == person.Id);
        if (existingPersonIndex != -1)
        {
            _data.persons[existingPersonIndex] = person;
            return Task.FromResult(person);
        }

        return Task.FromResult<Model.Person>(null);
    }

    public Task<bool> TryDeleteById(int id, CancellationToken cancellationToken)
    {
        var removedCount = _data.persons.RemoveAll(person => person.Id == id);
        return Task.FromResult(removedCount > 0);
    }
}
