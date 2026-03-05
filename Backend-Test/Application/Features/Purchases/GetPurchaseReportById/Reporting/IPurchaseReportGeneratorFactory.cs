using BackendTest.Model;

namespace BackendTest.Application.Features.Purchases.GetPurchaseReportById.Reporting;

public interface IPurchaseReportGeneratorFactory
{
    IPurchaseReportGenerator Create(Report.ReportFormat format);
}
