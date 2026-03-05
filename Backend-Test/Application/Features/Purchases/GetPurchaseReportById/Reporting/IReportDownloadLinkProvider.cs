namespace BackendTest.Application.Features.Purchases.GetPurchaseReportById.Reporting;

public interface IReportDownloadLinkProvider
{
    Task<string> CreateDownloadUrl(int purchaseId, Guid reportId, CancellationToken cancellationToken);
}
