using BackendTest.Application.Requests.Purchase;

namespace BackendTest.Application.Reporting;

public interface IPurchaseReportDataSource
{
    Task<PurchaseReportData> GetData(GetPurchaseReportByIdRequest request, CancellationToken cancellationToken);
}
