using BackendTest.Application.Features.Persons;
using BackendTest.Application.Features.Products;
using BackendTest.Application.Features.Purchases;
using BackendTest.Application.Features.Purchases.GetPurchaseReportById.Reporting;
using BackendTest.Infrastructure.Features.Persons;
using BackendTest.Infrastructure.Features.Products;
using BackendTest.Infrastructure.Features.Purchases;

namespace BackendTest.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<Data>();
        services.AddSingleton<IReportDownloadLinkProvider, InMemoryReportDownloadLinkProvider>();
        services.AddSingleton<IReportRepository, ReportRepository>();
        services.AddTransient<IPersonRepository, PersonRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IPurchaseRepository, PurchaseRepository>();

        return services;
    }
}
