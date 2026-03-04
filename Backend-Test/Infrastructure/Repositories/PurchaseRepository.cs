using BackendTest.Application.Repositories;

namespace BackendTest.Infrastructure.Repositories;

public class PurchaseRepository : IPurchaseRepository
{
    private readonly Data _data;

    public PurchaseRepository(Data data)
    {
        _data = data;
    }

    public bool Exists(int id, CancellationToken cancellationToken)
    {
        return _data.purchases.Any(purchase => purchase.Id == id);
    }

    public bool ExistsByCustomerId(int customerId, CancellationToken cancellationToken)
    {
        return _data.purchases.Any(purchase => purchase.CustomerId == customerId);
    }

    public bool ExistsByProductId(int productId, CancellationToken cancellationToken)
    {
        return _data.purchases.Any(purchase => purchase.ProductsIds.Contains(productId));
    }

    public List<Model.Purchase> GetAll(CancellationToken cancellationToken)
    {
        return _data.purchases;
    }

    public Model.Purchase GetById(int id, CancellationToken cancellationToken)
    {
        return _data.purchases.FirstOrDefault(purchase => purchase.Id == id);
    }

    public List<Model.Purchase> GetByCustomerId(int customerId, CancellationToken cancellationToken)
    {
        return _data.purchases.Where(purchase => purchase.CustomerId == customerId).ToList();
    }

    public void Add(Model.Purchase purchase, CancellationToken cancellationToken)
    {
        _data.purchases.Add(purchase);
    }

    public void DeleteById(int id, CancellationToken cancellationToken)
    {
        var purchase = GetById(id, cancellationToken);
        if (purchase != null)
        {
            _data.purchases.Remove(purchase);
        }
    }

    public void DeleteByCustomerId(int customerId, CancellationToken cancellationToken)
    {
        var purchases = GetByCustomerId(customerId, cancellationToken);
        foreach (var purchase in purchases)
        {
            _data.purchases.Remove(purchase);
        }
    }
}