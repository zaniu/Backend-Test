using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BackendTest.Test.IntegrationTests;

public abstract class IntegrationTestBase
{
    // create a new application and client per call so tests never share state
    protected HttpClient CreateClient()
    {
        var factory = new WebApplicationFactory<Program>();
        return factory.CreateClient();
    }

    protected async Task<HttpResponseMessage> GetAsync(HttpClient client, string url)
    {
        return await client.GetAsync(url);
    }

    protected async Task<HttpResponseMessage> PostAsync(HttpClient client, string url, object body = null)
    {
        StringContent content = null;
        if (body != null)
        {
            var json = JsonSerializer.Serialize(body);
            content = new StringContent(json, Encoding.UTF8, "application/json");
        }
        return await client.PostAsync(url, content);
    }

    protected async Task<HttpResponseMessage> DeleteAsync(HttpClient client, string url)
    {
        return await client.DeleteAsync(url);
    }

    protected static async Task<string> ReadAsStringAsync(HttpResponseMessage response)
    {
        return await response.Content.ReadAsStringAsync();
    }

    protected static async Task<T> ReadAsJsonAsync<T>(HttpResponseMessage response)
    {
        var jsonContent = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        return JsonSerializer.Deserialize<T>(jsonContent, options);
    }
}