using BackendTest.Model;

namespace BackendTest.Application.Features.Purchases.GetPurchaseReportById.Reporting;

public interface IPurchaseReportGenerator
{
    Report.ReportFormat Format { get; }
    Report Generate(PurchaseReportData data);
}
