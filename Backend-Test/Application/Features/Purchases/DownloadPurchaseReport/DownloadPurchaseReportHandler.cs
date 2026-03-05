using BackendTest.Exceptions;
using MediatR;

namespace BackendTest.Application.Features.Purchases.DownloadPurchaseReport;

public class DownloadPurchaseReportHandler : IRequestHandler<DownloadPurchaseReportRequest, DownloadPurchaseReportResponse>
{
    private readonly IReportRepository _reportRepository;

    public DownloadPurchaseReportHandler(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<DownloadPurchaseReportResponse> Handle(DownloadPurchaseReportRequest request, CancellationToken cancellationToken)
    {
        var report = await _reportRepository.GetById(request.ReportId, request.PurchaseId, cancellationToken);
        if (report == null)
        {
            throw new NotFoundException();
        }

        var reportFileName = $"purchase_report_{report.Id}.{report.FileExtension}";

        return new DownloadPurchaseReportResponse(report.Content, report.ContentType, reportFileName);
    }
}
