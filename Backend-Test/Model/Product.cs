namespace PersonApi.Models;

public class Product
{
    public int Id { get; init; }

    public string Name { get; init; }

    public string Type { get; init; }

    public decimal Price { get; set; }

    public Product() { }
    public Product(int id, string name, string type)
    {
        Id = id;
        Name = name;
        Type = type;
    }
}
