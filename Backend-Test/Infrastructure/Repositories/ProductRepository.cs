using BackendTest.Application.Repositories;

namespace BackendTest.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly Data _data;

    public ProductRepository(Data data)
    {
        _data = data;
    }

    public bool Exists(int id, CancellationToken cancellationToken)
    {
        return _data.products.Any(product => product.Id == id);
    }

    public List<Model.Product> GetAll(CancellationToken cancellationToken)
    {
        return _data.products;
    }

    public Model.Product GetById(int id, CancellationToken cancellationToken)
    {
        return _data.products.FirstOrDefault(product => product.Id == id);
    }

    public void Add(Model.Product product, CancellationToken cancellationToken)
    {
        _data.products.Add(product);
    }

    public void Update(Model.Product product, CancellationToken cancellationToken)
    {
        var existingProductIndex = _data.products.FindIndex(p => p.Id == product.Id);
        if (existingProductIndex != -1)
        {
            _data.products[existingProductIndex] = product;
        }
    }

    public void DeleteById(int id, CancellationToken cancellationToken)
    {
        var product = GetById(id, cancellationToken);
        if (product != null)
        {
            _data.products.Remove(product);
        }
    }
}