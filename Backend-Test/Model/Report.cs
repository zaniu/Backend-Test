namespace BackendTest.Model;

public class Report
{
    public Guid Id { get; init; }
    public byte[] Content { get; init; }
    public string ContentType { get; init; }
    public string FileExtension { get; init; }
    public int PurchaseId { get; init; }
    public ReportFormat Format { get; init; }

    public Report(Guid id, int purchaseId, ReportFormat format, byte[] content, string contentType, string fileExtension)
    {
        Id = id;
        PurchaseId = purchaseId;
        Format = format;
        Content = content;
        ContentType = contentType;
        FileExtension = fileExtension;
    }

    public enum ReportFormat
    {
        Csv = 1
    }
}
