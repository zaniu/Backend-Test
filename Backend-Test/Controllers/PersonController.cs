using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using PersonApi.Models;

namespace BackendTest.Controllers;

[ApiController]
[Route("persons")]
public class PersonController : ControllerBase
{
    static Data data = new();
    HelperUtils helper = new HelperUtils(data);
    CommonExceptions exceptions = new CommonExceptions();

    [HttpGet("persons/getAll/")]
    public ActionResult<IEnumerable<ObjPerson>> GetAll()
    {
        return data.persons;
    }

    [HttpGet("persons/get/{id}")]
    public ActionResult<ObjPerson> GetById(int id)
    {
        if(!helper.PersonExists(id))
        {
            exceptions.ItemNotExists();
            // Exception has no return
            return null;
        }
        else
        {
            return data.persons.First(s => s.Id == id);
        }
    }

    [HttpPost("persons/add/")]
    public ActionResult Add(ObjPerson person)
    {
        data.persons.Add(person);
        return Accepted(data.persons);
    }

    [HttpPost("persons/update/{id}")]
    public ActionResult Update(int id, ObjPerson person)
    {
        if (id != person.Id)
        {
            exceptions.IdDoesNotMatch();
        }
        if (person.YearOfBirth > DateAndTime.Now.Year)
        {
            throw new System.Exception("Customer can not be born after current year");
        }

        data.persons[id] = new ObjPerson(person.Id, person.Firstname, person.Lastname, person.YearOfBirth);

        return Accepted(data.persons);
    }

    [HttpDelete("persons/delete/{id}")]
    public ActionResult Delete(int id)
    {
        if (helper.PersonExists(id))
        {
            data.persons.Remove(data.persons.First(s => s.Id == id));
        }
        else
        {
            exceptions.ItemNotExists();
        }
        return Accepted(data.persons);
    }
}
