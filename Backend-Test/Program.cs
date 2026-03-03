using BackendTest;
using BackendTest.Application;
using BackendTest.Application.Validators;
using BackendTest.Middleware;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
WebApplication app = ConfigureServices(builder);
Configure(app);
app.Run();

static WebApplication ConfigureServices(WebApplicationBuilder builder)
{
    var envSection = builder.Configuration.GetSection("EnvironmentConfiguration");
    var envConfig = envSection.Get<BackendTest.Model.Environment>() ?? new BackendTest.Model.Environment();
    envConfig.IsProduction = builder.Environment.IsProduction();
    builder.Services.Configure((BackendTest.Model.Environment config) =>
    {
        config.ApiVersion = envConfig.ApiVersion;
        config.UiVersion = envConfig.UiVersion;
        config.IsProduction = envConfig.IsProduction;
    });


    builder.Services.Configure<BackendTest.Model.Environment>(builder.Configuration.GetSection("EnvironmentConfiguration"));
    builder.Services.PostConfigure((BackendTest.Model.Environment config) =>
    {
        config.IsProduction = builder.Environment.IsProduction();
    });

    builder.Services.AddSingleton<Data>();
    builder.Services.AddTransient<HelperUtils>();

    builder.Services.AddBackendTestApplication();

    builder.Services.AddControllers();
    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddValidatorsFromAssemblyContaining<ValidatorsAssemblyMarker>();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "BackendTest",
            Version = envConfig.ApiVersion
        });
    });

    var app = builder.Build();
    return app;
}

static void Configure(WebApplication app)
{
    app.UseMiddleware<ExceptionHandlingMiddleware>();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackendTest v1"));
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();

    app.MapControllers();
}