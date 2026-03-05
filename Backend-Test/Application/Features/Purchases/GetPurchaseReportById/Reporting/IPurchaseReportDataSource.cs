namespace BackendTest.Application.Features.Purchases.GetPurchaseReportById.Reporting;

public interface IPurchaseReportDataSource
{
    Task<PurchaseReportData> GetData(GetPurchaseReportByIdRequest request, CancellationToken cancellationToken);
}
