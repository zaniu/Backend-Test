using BackendTest.Application.Responses.Purchase;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BackendTest.Application.Requests.Purchase;

public record DownloadPurchaseReportRequest(int PurchaseId, Guid ReportId) : IRequest<DownloadPurchaseReportResponse>;