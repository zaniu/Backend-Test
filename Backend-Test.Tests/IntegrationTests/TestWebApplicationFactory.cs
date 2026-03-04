using BackendTest.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BackendTest.Test.IntegrationTests;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((_, configurationBuilder) =>
        {
            var deterministicSettings = new Dictionary<string, string>
            {
                ["EnvironmentConfiguration:ApiVersion"] = "2.3",
                ["EnvironmentConfiguration:UiVersion"] = "4.7"
            };

            configurationBuilder.AddInMemoryCollection(deterministicSettings);
        });

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<Data>();
            services.AddSingleton(CreateDeterministicData());
        });
    }

    private static Data CreateDeterministicData()
    {
        return new Data
        {
            persons = [],
            products = [],
            purchases = []
        };
    }
}