using BackendTest.Model;

namespace BackendTest.Application.Reporting;

public interface IPurchaseReportGeneratorFactory
{
    IPurchaseReportGenerator Create(Report.ReportFormat format);
}
