namespace BackendTest.Application.Responses.Purchase;

public record DownloadPurchaseReportResponse(byte[] Content, string ContentType, string FileName);