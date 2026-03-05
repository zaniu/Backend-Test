using BackendTest.Exceptions;
using BackendTest.Model;

namespace BackendTest.Application.Features.Purchases.GetPurchaseReportById.Reporting;

public class PurchaseReportGeneratorFactory : IPurchaseReportGeneratorFactory
{
    private readonly IReadOnlyCollection<IPurchaseReportGenerator> _generators;

    public PurchaseReportGeneratorFactory(IEnumerable<IPurchaseReportGenerator> generators)
    {
        _generators = generators.ToList();
    }

    public IPurchaseReportGenerator Create(Report.ReportFormat format)
    {
        var generator = _generators.FirstOrDefault(g => g.Format == format);
        if (generator == null)
        {
            throw new DomainModelException($"Report format '{format}' is not supported.");
        }

        return generator;
    }
}
