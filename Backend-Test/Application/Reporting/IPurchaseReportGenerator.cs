using BackendTest.Model;

namespace BackendTest.Application.Reporting;

public interface IPurchaseReportGenerator
{
    Report.ReportFormat Format { get; }
    Report Generate(PurchaseReportData data);
}
