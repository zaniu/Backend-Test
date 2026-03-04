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

    public List<Model.Purchase> GetAll(CancellationToken cancellationToken)
    {
        return _data.purchases;
    }

    public Model.Purchase GetById(int id, CancellationToken cancellationToken)
    {
        return _data.purchases.FirstOrDefault(purchase => purchase.Id == id);
    }

    public Model.Purchase GetByCustomerId(int customerId, CancellationToken cancellationToken)
    {
        return _data.purchases.FirstOrDefault(purchase => purchase.CustomerId == customerId);
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
        var purchase = GetByCustomerId(customerId, cancellationToken);
        if (purchase != null)
        {
            _data.purchases.Remove(purchase);
        }
    }
}