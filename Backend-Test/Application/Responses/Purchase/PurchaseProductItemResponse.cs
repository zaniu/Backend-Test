namespace BackendTest.Application.Responses.Purchase;

public record PurchaseProductItemResponse(int ProductId, int Count)
{
	public PurchaseProductItemResponse(Model.PurchaseProductItem item)
		: this(item.ProductId, item.Count)
	{
	}
}
