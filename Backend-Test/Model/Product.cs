using System.Diagnostics.CodeAnalysis;

namespace BackendTest.Model;

public class Product
{
    public required int Id { get; set; }

    public required string Name { get; init; }

    public required string Type { get; init; }

    public decimal Price { get; set; }

    public Product() { }
    
    [SetsRequiredMembers]
    public Product(int id, string name, string type, decimal price = 0)
    {
        Id = id;
        Name = name;
        Type = type;
        Price = price;
    }
}
