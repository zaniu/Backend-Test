using PersonApi.Models;

namespace BackendTest;

/// <summary>
/// NOT PART OF TEST. MERELY TO SUPPLY DATA
/// </summary>
public class Data
{
    public List<ObjPerson> persons = new()
    {
        new ObjPerson { Id = 1, Firstname = "John", Lastname = "Doe", YearOfBirth = 1980 },
        new ObjPerson { Id = 2, Firstname = "Jane", Lastname = "Doe", YearOfBirth = 1985 },
        new ObjPerson { Id = 3, Firstname = "Bob", Lastname = "Smith", YearOfBirth = 1990 },
        new ObjPerson { Id = 4, Firstname = "Alice", Lastname = "Johnson", YearOfBirth = 1995 },
        new ObjPerson { Id = 5, Firstname = "Mike", Lastname = "Brown", YearOfBirth = 1982 },
        new ObjPerson { Id = 6, Firstname = "Samantha", Lastname = "Davis", YearOfBirth = 1987 },
        new ObjPerson { Id = 7, Firstname = "David", Lastname = "Wilson", YearOfBirth = 1992 },
        new ObjPerson { Id = 8, Firstname = "Emily", Lastname = "Taylor", YearOfBirth = 1997 },
        new ObjPerson { Id = 9, Firstname = "Chris", Lastname = "Anderson", YearOfBirth = 1984 },
        new ObjPerson { Id = 10, Firstname = "Jessica", Lastname = "Thomas", YearOfBirth = 1989 }
    };

    public List<ObjProduct> products = new()
    {
        new ObjProduct { Id = 1, Name = "Pipe Wrench", Type = "Plumbing" },
        new ObjProduct { Id = 2, Name = "Electric Drill", Type = "Electric" },
        new ObjProduct { Id = 3, Name = "Garden Hose", Type = "Gardening" },
        new ObjProduct { Id = 4, Name = "Toilet Plunger", Type = "Plumbing" },
        new ObjProduct { Id = 5, Name = "Electric Screwdriver", Type = "Electric" },
        new ObjProduct { Id = 6, Name = "Garden Shovel", Type = "Gardening" },
        new ObjProduct { Id = 7, Name = "Faucet", Type = "Plumbing" },
        new ObjProduct { Id = 8, Name = "Electric Saw", Type = "Electric" },
        new ObjProduct { Id = 9, Name = "Garden Gloves", Type = "Gardening" },
        new ObjProduct { Id = 10, Name = "Pipe Cutter", Type = "Plumbing" }
    };

    public List<ObjPurchase> purchases = new()
    {
        new ObjPurchase { Id = 1, CustomerId = 1, ProductId = new List<double>{ 1 } },
        new ObjPurchase { Id = 2, CustomerId = 1, ProductId = new List<double>{ 2 } },
        new ObjPurchase { Id = 3, CustomerId = 1, ProductId = new List<double>{ 3 } },
        new ObjPurchase { Id = 4, CustomerId = 2, ProductId = new List<double>{ 4 } },
        new ObjPurchase { Id = 5, CustomerId = 2, ProductId = new List<double>{ 5 } },
        new ObjPurchase { Id = 6, CustomerId = 3, ProductId = new List<double>{ 6 } },
        new ObjPurchase { Id = 7, CustomerId = 7, ProductId = new List<double>{ 7 } },
        new ObjPurchase { Id = 8, CustomerId = 7, ProductId = new List<double>{ 8 } },
        new ObjPurchase { Id = 9, CustomerId = 4, ProductId = new List<double>{ 9 } },
        new ObjPurchase { Id = 10, CustomerId = 4, ProductId = new List<double>{ 10 } },
        new ObjPurchase { Id = 11, CustomerId = 4, ProductId = new List<double>{ 4 } },
        new ObjPurchase { Id = 12, CustomerId = 4, ProductId = new List<double>{ 8 } },
        new ObjPurchase { Id = 13, CustomerId = 8, ProductId = new List<double>{ 8 } },
        new ObjPurchase { Id = 14, CustomerId = 8, ProductId = new List<double>{ 2 } },
        new ObjPurchase { Id = 15, CustomerId = 5, ProductId = new List<double>{ 1 } },
        new ObjPurchase { Id = 16, CustomerId = 5, ProductId = new List<double>{ 6 } },
        new ObjPurchase { Id = 17, CustomerId = 8, ProductId = new List<double>{ 5 } },
        new ObjPurchase { Id = 18, CustomerId = 1, ProductId = new List<double>{ 4 } },
        new ObjPurchase { Id = 19, CustomerId = 2, ProductId = new List<double>{ 6 } },
        new ObjPurchase { Id = 20, CustomerId = 3, ProductId = new List<double>{ 10 } },
        new ObjPurchase { Id = 21, CustomerId = 4, ProductId = new List<double>{ 3 } },
        new ObjPurchase { Id = 22, CustomerId = 5, ProductId = new List<double>{ 1 } },
        new ObjPurchase { Id = 23, CustomerId = 1, ProductId = new List<double>{ 6 } },
        new ObjPurchase { Id = 24, CustomerId = 2, ProductId = new List<double>{ 10 } },
        new ObjPurchase { Id = 25, CustomerId = 3, ProductId = new List<double>{ 7 } },
        new ObjPurchase { Id = 26, CustomerId = 4, ProductId = new List<double>{ 1 } },
        new ObjPurchase { Id = 27, CustomerId = 5, ProductId = new List<double>{ 6 } },
        new ObjPurchase { Id = 28, CustomerId = 1, ProductId = new List<double>{ 10 } },
        new ObjPurchase { Id = 29, CustomerId = 2, ProductId = new List<double>{ 7 } },
        new ObjPurchase { Id = 30, CustomerId = 3, ProductId = new List<double>{ 1 } },
        new ObjPurchase { Id = 31, CustomerId = 4, ProductId = new List<double>{ 6 } },
        new ObjPurchase { Id = 32, CustomerId = 5, ProductId = new List<double>{ 10 } },
        new ObjPurchase { Id = 33, CustomerId = 1, ProductId = new List<double>{ 7 } },
        new ObjPurchase { Id = 34, CustomerId = 2, ProductId = new List<double>{ 1 } },
        new ObjPurchase { Id = 35, CustomerId = 3, ProductId = new List<double>{ 6 } },
        new ObjPurchase { Id = 36, CustomerId = 4, ProductId = new List<double>{ 10 } },
        new ObjPurchase { Id = 37, CustomerId = 6, ProductId = new List<double>{ 1 } },
        new ObjPurchase { Id = 38, CustomerId = 6, ProductId = new List<double>{ 4 } },
        new ObjPurchase { Id = 39, CustomerId = 6, ProductId = new List<double>{ 7 } }
    };
}
