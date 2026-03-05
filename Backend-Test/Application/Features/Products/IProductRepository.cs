namespace BackendTest.Application.Features.Products;

public interface IProductRepository
{
    Task<bool> Exists(int id, CancellationToken cancellationToken);
    Task<bool> ExistsAll(IEnumerable<int> ids, CancellationToken cancellationToken);
    Task<List<Model.Product>> GetAll(CancellationToken cancellationToken);
    Task<List<Model.Product>> GetByIds(IEnumerable<int> ids, CancellationToken cancellationToken);
    Task<Model.Product> GetById(int id, CancellationToken cancellationToken);
    Task<Model.Product> TryAdd(Model.Product product, CancellationToken cancellationToken);
    Task<Model.Product> TryUpdate(Model.Product product, CancellationToken cancellationToken);
    Task<bool> TryDeleteById(int id, CancellationToken cancellationToken);
}
