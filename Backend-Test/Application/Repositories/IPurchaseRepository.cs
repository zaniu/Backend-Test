namespace BackendTest.Application.Repositories;

public interface IPurchaseRepository
{
    Task<bool> Exists(int id, CancellationToken cancellationToken);
    Task<bool> ExistsByCustomerId(int customerId, CancellationToken cancellationToken);
    Task<bool> ExistsByProductId(int productId, CancellationToken cancellationToken);
    Task<List<Model.Purchase>> GetAll(CancellationToken cancellationToken);
    Task<Model.Purchase> GetById(int id, CancellationToken cancellationToken);
    Task<List<Model.Purchase>> GetByCustomerId(int customerId, CancellationToken cancellationToken);
    Task<Model.Purchase> TryAdd(Model.Purchase purchase, CancellationToken cancellationToken);
    Task<bool> TryDeleteById(int id, CancellationToken cancellationToken);
    Task<bool> TryDeleteByCustomerId(int customerId, CancellationToken cancellationToken);
}