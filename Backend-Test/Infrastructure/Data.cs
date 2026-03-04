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
        new Product { Id = 1, Name = "Pipe Wrench", Type = "Plumbing", Price = 19.99m },
        new Product { Id = 2, Name = "Electric Drill", Type = "Electric", Price = 49.99m },
        new Product { Id = 3, Name = "Garden Hose", Type = "Gardening", Price = 29.99m },
        new Product { Id = 4, Name = "Toilet Plunger", Type = "Plumbing", Price = 9.99m },
        new Product { Id = 5, Name = "Electric Screwdriver", Type = "Electric", Price = 39.99m },
        new Product { Id = 6, Name = "Garden Shovel", Type = "Gardening", Price = 24.99m },
        new Product { Id = 7, Name = "Faucet", Type = "Plumbing", Price = 14.99m },
        new Product { Id = 8, Name = "Electric Saw", Type = "Electric", Price = 59.99m },
        new Product { Id = 9, Name = "Garden Gloves", Type = "Gardening", Price = 12.99m },
        new Product { Id = 10, Name = "Pipe Cutter", Type = "Plumbing", Price = 19.99m }
    ];

    public List<Purchase> purchases =
    [
        new Purchase(1, 1, [new PurchaseProductItem(1, 1), new PurchaseProductItem(2, 1)]),
        new Purchase(2, 1, [new PurchaseProductItem(2, 1)]),
        new Purchase(3, 1, [new PurchaseProductItem(3, 1)]),
        new Purchase(4, 2, [new PurchaseProductItem(4, 1)]),
        new Purchase(5, 2, [new PurchaseProductItem(5, 1)]),
        new Purchase(6, 3, [new PurchaseProductItem(6, 1)]),
        new Purchase(7, 7, [new PurchaseProductItem(7, 1)]),
        new Purchase(8, 7, [new PurchaseProductItem(8, 1)]),
        new Purchase(9, 4, [new PurchaseProductItem(9, 1)]),
        new Purchase(10, 4, [new PurchaseProductItem(10, 1)]),
        new Purchase(11, 4, [new PurchaseProductItem(4, 1)]),
        new Purchase(12, 4, [new PurchaseProductItem(8, 1)]),
        new Purchase(13, 8, [new PurchaseProductItem(8, 1)]),
        new Purchase(14, 8, [new PurchaseProductItem(2, 1)]),
        new Purchase(15, 5, [new PurchaseProductItem(1, 1)]),
        new Purchase(16, 5, [new PurchaseProductItem(6, 1)]),
        new Purchase(17, 8, [new PurchaseProductItem(5, 1)]),
        new Purchase(18, 1, [new PurchaseProductItem(4, 1)]),
        new Purchase(19, 2, [new PurchaseProductItem(6, 1)]),
        new Purchase(20, 3, [new PurchaseProductItem(10, 1)]),
        new Purchase(21, 4, [new PurchaseProductItem(3, 1)]),
        new Purchase(22, 5, [new PurchaseProductItem(1, 1)]),
        new Purchase(23, 1, [new PurchaseProductItem(6, 1)]),
        new Purchase(24, 2, [new PurchaseProductItem(10, 1)]),
        new Purchase(25, 3, [new PurchaseProductItem(7, 1)]),
        new Purchase(26, 4, [new PurchaseProductItem(1, 1)]),
        new Purchase(27, 5, [new PurchaseProductItem(6, 1)]),
        new Purchase(28, 1, [new PurchaseProductItem(10, 1)]),
        new Purchase(29, 2, [new PurchaseProductItem(7, 1)]),
        new Purchase(30, 3, [new PurchaseProductItem(1, 1)]),
        new Purchase(31, 4, [new PurchaseProductItem(6, 1)]),
        new Purchase(32, 5, [new PurchaseProductItem(10, 1)]),
        new Purchase(33, 1, [new PurchaseProductItem(7, 1)]),
        new Purchase(34, 2, [new PurchaseProductItem(1, 1)]),
        new Purchase(35, 3, [new PurchaseProductItem(6, 1)]),
        new Purchase(36, 4, [new PurchaseProductItem(10, 1)]),
        new Purchase(37, 6, [new PurchaseProductItem(1, 1)]),
        new Purchase(38, 6, [new PurchaseProductItem(4, 1)]),
        new Purchase(39, 6, [new PurchaseProductItem(7, 1)])
    ];

    public List<Report> reports = [];
}
