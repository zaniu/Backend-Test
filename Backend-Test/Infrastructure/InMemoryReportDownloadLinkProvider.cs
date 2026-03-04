using BackendTest.Application.Handlers.Purchase;
using BackendTest.Application.Repositories;

namespace BackendTest.Infrastructure;

public class InMemoryReportDownloadLinkProvider : IReportDownloadLinkProvider
{
    public Task<string> CreateDownloadUrl(int purchaseId, Guid reportId, CancellationToken cancellationToken)
    {
        return Task.FromResult($"/purchases/{purchaseId}/reports/{reportId}");
    }
}