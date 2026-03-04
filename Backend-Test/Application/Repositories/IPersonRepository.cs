namespace BackendTest.Application.Repositories;

public interface IPersonRepository
{
    Task<bool> Exists(int id, CancellationToken cancellationToken);
    Task<List<Model.Person>> GetAll(CancellationToken cancellationToken);
    Task<Model.Person> GetById(int id, CancellationToken cancellationToken);
    Task<Model.Person> TryAdd(Model.Person person, CancellationToken cancellationToken);
    Task<Model.Person> TryUpdate(Model.Person person, CancellationToken cancellationToken);
    Task<bool> TryDeleteById(int id, CancellationToken cancellationToken);
}