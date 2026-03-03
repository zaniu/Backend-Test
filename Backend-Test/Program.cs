using BackendTest.Model;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
WebApplication app = ConfigureServices(builder);
Configure(app);
app.Run();

static WebApplication ConfigureServices(WebApplicationBuilder builder)
{
    var envSection = builder.Configuration.GetSection("EnvironmentConfiguration");
    var envConfig = envSection.Get<EnvironmentConfiguration>() ?? new EnvironmentConfiguration();
    envConfig.IsProduction = builder.Environment.IsProduction();
    builder.Services.Configure<EnvironmentConfiguration>(config =>
    {
        config.ApiVersion = envConfig.ApiVersion;
        config.UiVersion = envConfig.UiVersion;
        config.IsProduction = envConfig.IsProduction;
    });


    builder.Services.Configure<EnvironmentConfiguration>(builder.Configuration.GetSection("EnvironmentConfiguration"));
    builder.Services.PostConfigure<EnvironmentConfiguration>(config =>
    {
        config.IsProduction = builder.Environment.IsProduction();
    });

    builder.Services.AddControllers();
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
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackendTest v1"));
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();

    app.MapControllers();
}