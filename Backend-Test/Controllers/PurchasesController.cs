using Microsoft.AspNetCore.Mvc;
using PersonApi.Models;

namespace BackendTest.Controllers;

[Route("purchases")]
[ApiController]
public class PurchasesController : ControllerBase
{
    static Data data = new();
    HelperUtils helper = new HelperUtils(data);
    CommonExceptions exceptions = new CommonExceptions();

    [HttpGet("purchases/getAll/")]
    public ActionResult<IEnumerable<Purchase>> GetAll()
    {
        return data.purchases;
    }

    [HttpGet("purchases/get/{id}")]
    public ActionResult<Purchase> GetByCustomerId(int id)
    {
        if (data.purchases.First(s => s.CustomerId == id) == null)
        {
            exceptions.ItemNotExists();
            // Exception doesn't return
            return null;
        }
        else
        {
            return data.purchases.First(s => s.CustomerId == id);
        }
    }

    /// <summary>
    /// Generates a CSV report of a purchase, including a list of purchased items, their prices, the total expenditure, and customer information.
    /// </summary>
    /// <param name="id">The id of the Purchase order</param>
    [HttpGet("purchases/get/{id}/report")]
    public async Task<ActionResult<byte[]>> GetPurchaseReportById(int id)
    {
        throw new NotImplementedException("Please implement me!");
    }

    [HttpPost("purchases/add/")]
    public ActionResult Add(Purchase purchase)
    {
        data.purchases.Add(purchase);
        return Accepted(data.products);
    }

    [HttpDelete("purchases/delete/{id}")]
    public ActionResult Delete(int id)
    {
        data.purchases.Remove(data.purchases.First(s => s.Id == id));
        return Accepted(data.purchases);
    }

    [HttpDelete("purchases/delete/{customerId}")]
    public ActionResult DeleteFromCustomer(int id)
    {
        if (helper.PurchaseExists(id))
        {
            data.purchases.Remove(data.purchases.First(s => s.CustomerId == id));
        }
        else
        {
            exceptions.ItemNotExists();
        }
        return Accepted(data.purchases);
    }
}
