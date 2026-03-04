using BackendTest.Application.Repositories;

namespace BackendTest.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly Data _data;

    public ProductRepository(Data data)
    {
        _data = data;
    }

    public Task<bool> Exists(int id, CancellationToken cancellationToken)
    {
        return Task.FromResult(_data.products.Any(product => product.Id == id));
    }

    public Task<bool> ExistsAll(IEnumerable<int> ids, CancellationToken cancellationToken)
    {
        var existingProductIds = _data.products.Select(product => product.Id).ToHashSet();
        return Task.FromResult(ids.All(existingProductIds.Contains));
    }

    public Task<List<Model.Product>> GetAll(CancellationToken cancellationToken)
    {
        return Task.FromResult(_data.products.ToList());
    }

    public Task<List<Model.Product>> GetByIds(IEnumerable<int> ids, CancellationToken cancellationToken)
    {
        var idsSet = ids.ToHashSet();
        return Task.FromResult(_data.products.Where(product => idsSet.Contains(product.Id)).ToList());
    }

    public Task<Model.Product> GetById(int id, CancellationToken cancellationToken)
    {
        return Task.FromResult(_data.products.FirstOrDefault(product => product.Id == id));
    }

    public Task<Model.Product> TryAdd(Model.Product product, CancellationToken cancellationToken)
    {
        if (_data.products.Any(existingProduct => existingProduct.Id == product.Id))
        {
            return Task.FromResult<Model.Product>(null);
        }

        _data.products.Add(product);
        return Task.FromResult(product);
    }

    public Task<Model.Product> TryUpdate(Model.Product product, CancellationToken cancellationToken)
    {
        var existingProductIndex = _data.products.FindIndex(p => p.Id == product.Id);
        if (existingProductIndex != -1)
        {
            _data.products[existingProductIndex] = product;
            return Task.FromResult(product);
        }

        return Task.FromResult<Model.Product>(null);
    }

    public Task<bool> TryDeleteById(int id, CancellationToken cancellationToken)
    {
        var removedCount = _data.products.RemoveAll(product => product.Id == id);
        return Task.FromResult(removedCount > 0);
    }
}