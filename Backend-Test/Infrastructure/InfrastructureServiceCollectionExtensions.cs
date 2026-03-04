using BackendTest.Application.Repositories;
using BackendTest.Infrastructure.Repositories;

namespace BackendTest.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<Data>();
        services.AddTransient<IPersonRepository, PersonRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IPurchaseRepository, PurchaseRepository>();

        return services;
    }
}
