using Microsoft.AspNetCore.Mvc;
using PersonApi.Models;

namespace BackendTest.Controllers;

[ApiController]
[Route("persons")]
public class PersonController : ControllerBase
{
    private readonly Data _data;
    private readonly HelperUtils _helper;
    private readonly CommonExceptions _exceptions;

    public PersonController(Data data, HelperUtils helper, CommonExceptions exceptions)
    {
        _data = data;
        _helper = helper;
        _exceptions = exceptions;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Person>> GetAll()
    {
        return _data.persons;
    }

    [HttpGet("{id}")]
    public ActionResult<Person> GetById(int id)
    {
        if(!_helper.PersonExists(id))
        {
            _exceptions.ItemNotExists();
            // Exception has no return
            return null;
        }
        else
        {
            return _data.persons.First(s => s.Id == id);
        }
    }

    [HttpPost]
    public ActionResult Add(Person person)
    {
        _data.persons.Add(person);
        return Accepted(_data.persons);
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, Person person)
    {
        if (id != person.Id)
        {
            _exceptions.IdDoesNotMatch();
        }
        if (person.YearOfBirth > DateTime.UtcNow.Year)
        {
            throw new Exception("Customer can not be born after current year");
        }

        _data.persons[id] = new Person(person.Id, person.Firstname, person.Lastname, person.YearOfBirth);

        return Accepted(_data.persons);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        if (_helper.PersonExists(id))
        {
            _data.persons.Remove(_data.persons.First(s => s.Id == id));
        }
        else
        {
            _exceptions.ItemNotExists();
        }
        return Accepted(_data.persons);
    }
}
