namespace BackendTest.Application.Repositories;

public interface IPurchaseRepository
{
    bool Exists(int id, CancellationToken cancellationToken);
    List<Model.Purchase> GetAll(CancellationToken cancellationToken);
    Model.Purchase GetById(int id, CancellationToken cancellationToken);
    List<Model.Purchase> GetByCustomerId(int customerId, CancellationToken cancellationToken);
    void Add(Model.Purchase purchase, CancellationToken cancellationToken);
    void DeleteById(int id, CancellationToken cancellationToken);
    void DeleteByCustomerId(int customerId, CancellationToken cancellationToken);
}