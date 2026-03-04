namespace BackendTest.Application.Repositories;

public interface IReportDownloadLinkProvider
{
    Task<string> CreateDownloadUrl(int purchaseId, Guid reportId, CancellationToken cancellationToken);
}