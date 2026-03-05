using MediatR;

namespace BackendTest.Application.Features.Purchases.GetPurchaseReportById;

public record GetPurchaseReportByIdRequest(int PurchaseId, GetPurchaseReportByIdRequest.ReportFormat Format = GetPurchaseReportByIdRequest.ReportFormat.Csv) : IRequest<GetPurchaseReportByIdResponse>
{
    public enum ReportFormat
    {
        Csv
    }
}
