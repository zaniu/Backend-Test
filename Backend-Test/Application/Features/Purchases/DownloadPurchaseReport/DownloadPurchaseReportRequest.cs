using MediatR;

namespace BackendTest.Application.Features.Purchases.DownloadPurchaseReport;

public record DownloadPurchaseReportRequest(int PurchaseId, Guid ReportId) : IRequest<DownloadPurchaseReportResponse>;
