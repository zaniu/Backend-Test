using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace BackendTest.Model;

public class Purchase
{
    private readonly List<int> _productsIds;

    public required int Id { get; init; }

    public required int CustomerId { get; init; }

    public ReadOnlyCollection<int> ProductsIds { get => _productsIds.AsReadOnly(); }

    [SetsRequiredMembers]
    public Purchase(int id, int customerId, List<int> productsIds)
    {
        Id = id;
        CustomerId = customerId;
        _productsIds = productsIds;
    }
}
