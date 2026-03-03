using System.Diagnostics.CodeAnalysis;
using BackendTest.Exceptions;

namespace BackendTest.Model;

public class Person
{
    public required int Id { get; set; }

    public required string Firstname { get; set; }

    public required string Lastname { get; set; }

    public int YearOfBirth
    {
        get { return field; }
        set
        {
            if (value > DateTime.UtcNow.Year)
            {
                throw new DomainModelException("Customer can not be born after current year");
            }
            field = value;
        }
    }

    public Person() { }
    
    [SetsRequiredMembers]
    public Person(int id, string firstname, string lastname, int yearOfBirth)
    {
        Id = id;
        Firstname = firstname;
        Lastname = lastname;
        YearOfBirth = yearOfBirth;
    }
}
