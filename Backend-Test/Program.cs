using BackendTest.Application;
using BackendTest.Extensions;
using BackendTest.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddInfrastructure()
    .AddBackendTestApplication()
    .AddPresentation(builder.Configuration, builder.Environment);

var app = builder.Build();
app.UsePresentation();
app.Run();
