using MediatR;
using BackendTest.Application.Responses.Purchase;

namespace BackendTest.Application.Requests.Purchase;

public record GetPurchaseReportByIdRequest(int PurchaseId, GetPurchaseReportByIdRequest.ReportFormat Format = GetPurchaseReportByIdRequest.ReportFormat.Csv) : IRequest<GetPurchaseReportByIdResponse>
{
    public enum ReportFormat
    {
        Csv
    }
}