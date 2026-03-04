using BackendTest.Application.Repositories;

namespace BackendTest.Infrastructure.Repositories;

public class PurchaseRepository : IPurchaseRepository
{
    private readonly Data _data;

    public PurchaseRepository(Data data)
    {
        _data = data;
    }

    public Task<bool> Exists(int id, CancellationToken cancellationToken)
    {
        return Task.FromResult(_data.purchases.Any(purchase => purchase.Id == id));
    }

    public Task<bool> ExistsByCustomerId(int customerId, CancellationToken cancellationToken)
    {
        return Task.FromResult(_data.purchases.Any(purchase => purchase.CustomerId == customerId));
    }

    public Task<bool> ExistsByProductId(int productId, CancellationToken cancellationToken)
    {
        return Task.FromResult(_data.purchases.Any(purchase => purchase.ProductsIds.Contains(productId)));
    }

    public Task<List<Model.Purchase>> GetAll(CancellationToken cancellationToken)
    {
        return Task.FromResult(_data.purchases.ToList());
    }

    public Task<Model.Purchase> GetById(int id, CancellationToken cancellationToken)
    {
        return Task.FromResult(_data.purchases.FirstOrDefault(purchase => purchase.Id == id));
    }

    public Task<List<Model.Purchase>> GetByCustomerId(int customerId, CancellationToken cancellationToken)
    {
        return Task.FromResult(_data.purchases.Where(purchase => purchase.CustomerId == customerId).ToList());
    }

    public Task<Model.Purchase> TryAdd(Model.Purchase purchase, CancellationToken cancellationToken)
    {
        if (_data.purchases.Any(existingPurchase => existingPurchase.Id == purchase.Id))
        {
            return Task.FromResult<Model.Purchase>(null);
        }

        _data.purchases.Add(purchase);
        return Task.FromResult(purchase);
    }

    public Task<bool> TryDeleteById(int id, CancellationToken cancellationToken)
    {
        var removedCount = _data.purchases.RemoveAll(purchase => purchase.Id == id);
        return Task.FromResult(removedCount > 0);
    }

    public Task<bool> TryDeleteByCustomerId(int customerId, CancellationToken cancellationToken)
    {
        var removedCount = _data.purchases.RemoveAll(purchase => purchase.CustomerId == customerId);
        return Task.FromResult(removedCount > 0);
    }
}