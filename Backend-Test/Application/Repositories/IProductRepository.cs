namespace BackendTest.Application.Repositories;

public interface IProductRepository
{
    bool Exists(int id, CancellationToken cancellationToken);
    List<Model.Product> GetAll(CancellationToken cancellationToken);
    Model.Product GetById(int id, CancellationToken cancellationToken);
    void Add(Model.Product product, CancellationToken cancellationToken);
    void Update(Model.Product product, CancellationToken cancellationToken);
    void DeleteById(int id, CancellationToken cancellationToken);
}