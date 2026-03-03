namespace BackendTest.Application;

public static class BackendTestApplication
{
    public static IServiceCollection AddBackendTestApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(BackendTestApplication).Assembly);
        });

        return services;
    }
}
