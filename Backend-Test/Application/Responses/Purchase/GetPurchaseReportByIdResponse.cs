namespace BackendTest.Application.Responses.Purchase;

public record GetPurchaseReportByIdResponse(int PurchaseId, Guid ReportId, string DownloadUrl = "");