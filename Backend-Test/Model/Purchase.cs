using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace BackendTest.Model;

public class Purchase
{
    private readonly List<PurchaseProductItem> _items;

    public required int Id { get; init; }

    public required int CustomerId { get; init; }

    public ReadOnlyCollection<PurchaseProductItem> Items { get => _items.AsReadOnly(); }

    [SetsRequiredMembers]
    public Purchase(int id, int customerId, List<PurchaseProductItem> items)
    {
        Id = id;
        CustomerId = customerId;
        _items = items;
    }
}
