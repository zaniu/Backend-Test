using BackendTest.Application.Features.Purchases.GetPurchaseReportById.Reporting;

namespace BackendTest.Infrastructure;

public class InMemoryReportDownloadLinkProvider : IReportDownloadLinkProvider
{
    public Task<string> CreateDownloadUrl(int purchaseId, Guid reportId, CancellationToken cancellationToken)
    {
        return Task.FromResult($"/purchases/{purchaseId}/reports/{reportId}");
    }
}
