using Microsoft.AspNetCore.Mvc;
using PersonApi.Models;

namespace BackendTest.Controllers;

[ApiController]
[Route("products")]
public class ProductsController : ControllerBase
{
    static Data data = new();
    HelperUtils helper = new HelperUtils(data);
    CommonExceptions exceptions = new CommonExceptions();

    [HttpGet("products/getAll/")]
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        return data.products;
    }

    [HttpGet("products/get/{id}")]
    public ActionResult<Product> GetById(int id)
    {
        if (!helper.ProductExists(id))
        {
            exceptions.ItemNotExists();
            // Exception has no return
            return null;
        }
        else
        {
            return data.products.First(s => s.Id == id);
        }
    }

    [HttpPost("products/add/")]
    public ActionResult Add(Product product)
    {
        data.products.Add(product);
        return Accepted(data.products);
    }

    [HttpPost("products/update/{id}")]
    public ActionResult Update(int id, Product product)
    {
        if (id != product.Id)
        {
            exceptions.IdDoesNotMatch();
        }

        data.products[id] = new Product(product.Id, product.Name, product.Type);

        return Accepted(data.products);
    }

    [HttpDelete("products/delete/{id}")]
    public ActionResult Delete(int id)
    {
        if (helper.ProductExists(id))
        {
            data.products.Remove(data.products.First(s => s.Id == id));
        }
        else
        {
            exceptions.ItemNotExists();
        }
        return Accepted(data.products);
    }
}
