using BackendTest.Model;

namespace BackendTest.Infrastructure;

/// <summary>
/// NOT PART OF TEST. MERELY TO SUPPLY DATA
/// </summary>
public class Data
{
    public List<Person> persons =
    [
        new Person { Id = 1, Firstname = "John", Lastname = "Doe", YearOfBirth = 1980 },
        new Person { Id = 2, Firstname = "Jane", Lastname = "Doe", YearOfBirth = 1985 },
        new Person { Id = 3, Firstname = "Bob", Lastname = "Smith", YearOfBirth = 1990 },
        new Person { Id = 4, Firstname = "Alice", Lastname = "Johnson", YearOfBirth = 1995 },
        new Person { Id = 5, Firstname = "Mike", Lastname = "Brown", YearOfBirth = 1982 },
        new Person { Id = 6, Firstname = "Samantha", Lastname = "Davis", YearOfBirth = 1987 },
        new Person { Id = 7, Firstname = "David", Lastname = "Wilson", YearOfBirth = 1992 },
        new Person { Id = 8, Firstname = "Emily", Lastname = "Taylor", YearOfBirth = 1997 },
        new Person { Id = 9, Firstname = "Chris", Lastname = "Anderson", YearOfBirth = 1984 },
        new Person { Id = 10, Firstname = "Jessica", Lastname = "Thomas", YearOfBirth = 1989 }
    ];

    public List<Product> products =
    [
        new Product { Id = 1, Name = "Pipe Wrench", Type = "Plumbing" },
        new Product { Id = 2, Name = "Electric Drill", Type = "Electric" },
        new Product { Id = 3, Name = "Garden Hose", Type = "Gardening" },
        new Product { Id = 4, Name = "Toilet Plunger", Type = "Plumbing" },
        new Product { Id = 5, Name = "Electric Screwdriver", Type = "Electric" },
        new Product { Id = 6, Name = "Garden Shovel", Type = "Gardening" },
        new Product { Id = 7, Name = "Faucet", Type = "Plumbing" },
        new Product { Id = 8, Name = "Electric Saw", Type = "Electric" },
        new Product { Id = 9, Name = "Garden Gloves", Type = "Gardening" },
        new Product { Id = 10, Name = "Pipe Cutter", Type = "Plumbing" }
    ];

    public List<Purchase> purchases =
    [
        new Purchase(1, 1, [1]),
        new Purchase(2, 1, [2]),
        new Purchase(3, 1, [3]),
        new Purchase(4, 2, [4]),
        new Purchase(5, 2, [5]),
        new Purchase(6, 3, [6]),
        new Purchase(7, 7, [7]),
        new Purchase(8, 7, [8]),
        new Purchase(9, 4, [9]),
        new Purchase(10, 4, [10]),
        new Purchase(11, 4, [4]),
        new Purchase(12, 4, [8]),
        new Purchase(13, 8, [8]),
        new Purchase(14, 8, [2]),
        new Purchase(15, 5, [1]),
        new Purchase(16, 5, [6]),
        new Purchase(17, 8, [5]),
        new Purchase(18, 1, [4]),
        new Purchase(19, 2, [6]),
        new Purchase(20, 3, [10]),
        new Purchase(21, 4, [3]),
        new Purchase(22, 5, [1]),
        new Purchase(23, 1, [6]),
        new Purchase(24, 2, [10]),
        new Purchase(25, 3, [7]),
        new Purchase(26, 4, [1]),
        new Purchase(27, 5, [6]),
        new Purchase(28, 1, [10]),
        new Purchase(29, 2, [7]),
        new Purchase(30, 3, [1]),
        new Purchase(31, 4, [6]),
        new Purchase(32, 5, [10]),
        new Purchase(33, 1, [7]),
        new Purchase(34, 2, [1]),
        new Purchase(35, 3, [6]),
        new Purchase(36, 4, [10]),
        new Purchase(37, 6, [1]),
        new Purchase(38, 6, [4]),
        new Purchase(39, 6, [7])
    ];
}
