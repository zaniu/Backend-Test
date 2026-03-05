namespace BackendTest.Application.Features.Purchases.DownloadPurchaseReport;

public record DownloadPurchaseReportResponse(byte[] Content, string ContentType, string FileName);
