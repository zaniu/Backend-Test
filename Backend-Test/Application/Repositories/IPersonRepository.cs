namespace BackendTest.Application.Repositories;

public interface IPersonRepository
{
    bool Exists(int id, CancellationToken cancellationToken);
    List<Model.Person> GetAll(CancellationToken cancellationToken);
    Model.Person GetById(int id, CancellationToken cancellationToken);
    void Add(Model.Person person, CancellationToken cancellationToken);
    void Update(Model.Person person, CancellationToken cancellationToken);
    void DeleteById(int id, CancellationToken cancellationToken);
}