using System.Collections.ObjectModel;

namespace PersonApi.Models;

public class Purchase
{
    private readonly List<int> _productsIds;
    
    public int Id { get; init; }

    public int CustomerId { get; init; }

    public ReadOnlyCollection<int> ProductsIds { get => _productsIds.AsReadOnly(); }

    public Purchase(int id, int customerId, List<int> productsIds)
    {
        Id = id;
        CustomerId = customerId;
        _productsIds = productsIds;
    }
}
