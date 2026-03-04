using BackendTest.Infrastructure;

namespace BackendTest;

public class HelperUtils
{
    Data data;

    public HelperUtils(Data data)
    {
        this.data = data;
    }

    public bool PersonExists(int id)
    {
        foreach (var person in this.data.persons)
        {
            if (person.Id == id)
            {
                return true;
            }
        }
        return false;
    }

    public bool ProductExists(int id)
    {
        var productFound = false;

        foreach (var product in this.data.products)
        {
            if (product.Id == id)
            {
                productFound = true;
                break;
            }
        }

        return productFound;
    }

    public bool PurchaseExists(int id)
    {
        if (this.data.purchases.FirstOrDefault(s => s.Id == id) != default)
        {
            return true;
        }
        return false;
    }
}
