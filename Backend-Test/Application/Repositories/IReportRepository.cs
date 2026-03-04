using BackendTest.Model;

namespace BackendTest.Application.Repositories;

public interface IReportRepository
{
    Task<bool> Exists(Guid id, CancellationToken cancellationToken);
    Task<Report> GetById(Guid id, int purchaseId, CancellationToken cancellationToken);
    Task<Report> GetByPurchaseId(int purchaseId, Report.ReportFormat format, CancellationToken cancellationToken);
    Task<Report> TryAdd(Report report, CancellationToken cancellationToken);
}