namespace BackendTest.Application.Features.Purchases.GetPurchaseReportById.Reporting;

public record PurchaseReportData(int PurchaseId, Model.Person Customer, IReadOnlyCollection<Model.PurchaseProductItem> Items, IReadOnlyDictionary<int, Model.Product> ProductsById);
