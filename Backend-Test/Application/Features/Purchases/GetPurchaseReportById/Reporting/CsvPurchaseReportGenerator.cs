using System.Globalization;
using System.Text;
using BackendTest.Model;
using static BackendTest.Model.Report;

namespace BackendTest.Application.Features.Purchases.GetPurchaseReportById.Reporting;

public class CsvPurchaseReportGenerator : IPurchaseReportGenerator
{
    public ReportFormat Format => ReportFormat.Csv;

    public Report Generate(PurchaseReportData data)
    {
        var csvBuilder = new StringBuilder();
        csvBuilder.AppendLine($"CustomerName:,{Escape($"{data.Customer.Firstname} {data.Customer.Lastname}")},,");
        csvBuilder.AppendLine("ProductId,Count,ProductName,Price");

        foreach (var item in data.Items)
        {
            var product = data.ProductsById[item.ProductId];

            csvBuilder.AppendLine(string.Join(",",
                item.ProductId.ToString(CultureInfo.InvariantCulture),
                item.Count.ToString(CultureInfo.InvariantCulture),
                Escape(product.Name),
                product.Price.ToString(CultureInfo.InvariantCulture)));
        }

        return new Report(Guid.CreateVersion7(), data.PurchaseId, Format, Encoding.UTF8.GetBytes(csvBuilder.ToString()), "text/csv", "csv");
    }

    private static string Escape(string value)
    {
        if (value.Contains(',') || value.Contains('"') || value.Contains('\n') || value.Contains('\r'))
        {
            return $"\"{value.Replace("\"", "\"\"")}\"";
        }

        return value;
    }
}
