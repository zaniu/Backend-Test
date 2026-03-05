using BackendTest.Application.Features.Persons;
using BackendTest.Application.Features.Products;
using BackendTest.Exceptions;

namespace BackendTest.Application.Features.Purchases.GetPurchaseReportById.Reporting;

public class DefaultPurchaseReportDataSource : IPurchaseReportDataSource
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IProductRepository _productRepository;

    public DefaultPurchaseReportDataSource(
        IPurchaseRepository purchaseRepository,
        IPersonRepository personRepository,
        IProductRepository productRepository)
    {
        _purchaseRepository = purchaseRepository;
        _personRepository = personRepository;
        _productRepository = productRepository;
    }

    public async Task<PurchaseReportData> GetData(GetPurchaseReportByIdRequest request, CancellationToken cancellationToken)
    {
        var purchase = await _purchaseRepository.GetById(request.PurchaseId, cancellationToken);
        if (purchase == null)
        {
            throw new NotFoundException();
        }

        var customer = await _personRepository.GetById(purchase.CustomerId, cancellationToken);
        if (customer == null)
        {
            throw new DomainModelException($"Customer with id '{purchase.CustomerId}' does not exist.");
        }

        var requestedProductIds = purchase.Items.Select(item => item.ProductId).Distinct().ToList();
        var productsById = (await _productRepository.GetByIds(requestedProductIds, cancellationToken))
            .ToDictionary(product => product.Id);

        foreach (var item in purchase.Items)
        {
            if (!productsById.ContainsKey(item.ProductId))
            {
                throw new DomainModelException($"Product with id '{item.ProductId}' does not exist.");
            }
        }

        return new PurchaseReportData(request.PurchaseId, customer, purchase.Items, productsById);
    }
}
