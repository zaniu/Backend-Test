using BackendTest.Application.Repositories;
using BackendTest.Application.Requests.Purchase;
using BackendTest.Application.Responses.Purchase;
using BackendTest.Application.Reporting;
using MediatR;
using BackendTest.Model;
using BackendTest.Exceptions;

namespace BackendTest.Application.Handlers.Purchase;

public class GetPurchaseReportByIdHandler : IRequestHandler<GetPurchaseReportByIdRequest, GetPurchaseReportByIdResponse>
{
    private static readonly SemaphoreSlim ReportCreationLock = new(1, 1);

    private readonly IPurchaseReportDataSource _reportDataSource;
    private readonly IReportRepository _reportRepository;
    private readonly IReportDownloadLinkProvider _reportDownloadLinkProvider;
    private readonly IPurchaseReportGeneratorFactory _reportGeneratorFactory;

    public GetPurchaseReportByIdHandler(
        IPurchaseReportDataSource reportDataSource,
        IReportRepository reportRepository,
        IReportDownloadLinkProvider reportDownloadLinkProvider,
        IPurchaseReportGeneratorFactory reportGeneratorFactory)
    {
        _reportDataSource = reportDataSource;
        _reportRepository = reportRepository;
        _reportDownloadLinkProvider = reportDownloadLinkProvider;
        _reportGeneratorFactory = reportGeneratorFactory;
    }

    public async Task<GetPurchaseReportByIdResponse> Handle(GetPurchaseReportByIdRequest request, CancellationToken cancellationToken)
    {
        var report = await GetOrCreateReport(request, cancellationToken);

        var downloadUrl = await _reportDownloadLinkProvider.CreateDownloadUrl(report.PurchaseId, report.Id, cancellationToken);

        return new GetPurchaseReportByIdResponse(report.PurchaseId, report.Id, downloadUrl);

    }

    private async Task<Report> GetOrCreateReport(GetPurchaseReportByIdRequest request, CancellationToken cancellationToken)
    {
        var reportFormat = ToDomain(request.Format);
        var existingReport = await _reportRepository.GetByPurchaseId(request.PurchaseId, reportFormat, cancellationToken);
        if (existingReport != null)
        {
            return existingReport;
        }

        await ReportCreationLock.WaitAsync(cancellationToken);
        try
        {
            existingReport = await _reportRepository.GetByPurchaseId(request.PurchaseId, reportFormat, cancellationToken);
            if (existingReport != null)
            {
                return existingReport;
            }

            var reportData = await _reportDataSource.GetData(request, cancellationToken);
            var reportGenerator = _reportGeneratorFactory.Create(reportFormat);
            var report = reportGenerator.Generate(reportData);

            var addedReport = await _reportRepository.TryAdd(report, cancellationToken);
            
            if (addedReport == null)
            {
                throw new DomainModelException("Failed to add report to repository");
            }
            return addedReport;
        }
        finally
        {
            ReportCreationLock.Release();
        }
    }

    private static Report.ReportFormat ToDomain(GetPurchaseReportByIdRequest.ReportFormat format)
    {
        return format switch
        {
            GetPurchaseReportByIdRequest.ReportFormat.Csv => Report.ReportFormat.Csv,
            _ => throw new DomainModelException($"Unsupported report format: {format}")
        };
    }
}