using BackendTest.Middleware;
using Microsoft.OpenApi.Models;

namespace BackendTest.Extensions;

public static class CompositionExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services.Configure<BackendTest.Model.Environment>(configuration.GetSection("EnvironmentConfiguration"));
        services.PostConfigure((BackendTest.Model.Environment config) =>
        {
            config.IsProduction = environment.IsProduction();
        });

        var apiVersion = configuration["EnvironmentConfiguration:ApiVersion"] ?? "v1";

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "BackendTest",
                Version = apiVersion
            });
        });

        return services;
    }

    public static WebApplication UsePresentation(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackendTest v1"));
        }

        if (!app.Environment.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }

        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}
