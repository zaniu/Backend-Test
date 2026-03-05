namespace BackendTest.Application.Features.Purchases.GetPurchaseReportById;

public record GetPurchaseReportByIdResponse(int PurchaseId, Guid ReportId, string DownloadUrl = "");
