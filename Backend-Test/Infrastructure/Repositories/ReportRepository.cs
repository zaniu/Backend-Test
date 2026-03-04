using BackendTest.Application.Repositories;
using BackendTest.Model;

namespace BackendTest.Infrastructure.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly Data _data;

    public ReportRepository(Data data)
    {
        _data = data;
    }

    public Task<bool> Exists(Guid id, CancellationToken cancellationToken)
    {
        return Task.FromResult(_data.reports.Any(report => report.Id == id));
    }

    public Task<Report> GetById(Guid id, int purchaseId, CancellationToken cancellationToken)
    {
        return Task.FromResult(_data.reports.FirstOrDefault(report => report.Id == id && report.PurchaseId == purchaseId));
    }

    public Task<Report> GetByPurchaseId(int purchaseId, Report.ReportFormat format, CancellationToken cancellationToken)
    {
        return Task.FromResult(_data.reports.FirstOrDefault(report => report.PurchaseId == purchaseId && report.Format == format));
    }

    public Task<Report> TryAdd(Report report, CancellationToken cancellationToken)
    {
        if (_data.reports.Any(existingReport => existingReport.Id == report.Id))
        {
            return Task.FromResult<Report>(null);
        }

        _data.reports.Add(report);
        return Task.FromResult(report);
    }
}