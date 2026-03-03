namespace BackendTest.Test.IntegrationTests;

/// <summary>
/// Test-only DTO used to deserialize purchase payloads in integration tests.
/// </summary>
public class PurchaseDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public List<int> ProductsIds { get; set; } = [];
}