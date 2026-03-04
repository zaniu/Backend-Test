using BackendTest.Application.Behaviors;
using BackendTest.Application.Validators;
using FluentValidation;

namespace BackendTest.Application;

public static class BackendTestApplication
{
    public static IServiceCollection AddBackendTestApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(ValidatorsAssemblyMarker).Assembly);

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(BackendTestApplication).Assembly);
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        return services;
    }
}
