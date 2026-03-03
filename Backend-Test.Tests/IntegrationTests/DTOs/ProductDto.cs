namespace BackendTest.Test.IntegrationTests;

/// <summary>
/// Test-only DTO used to deserialize product payloads in integration tests.
/// </summary>
public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public decimal Price { get; set; }
}