namespace PersonApi.Models;

public class Person
{
    public int Id { get; init; }

    public string Firstname { get; init; }

    public string Lastname { get; init; }

    public int YearOfBirth
    {
        get { return field; }
        init
        {
            if (value > DateTime.UtcNow.Year)
            {
                throw new Exception("Customer can not be born after current year");
            }
            field = value;
        }
    }

    public Person() { }
    public Person(int id, string firstname, string lastname, int yearOfBirth)
    {
        Id = id;
        Firstname = firstname;
        Lastname = lastname;
        YearOfBirth = yearOfBirth;
    }
}
