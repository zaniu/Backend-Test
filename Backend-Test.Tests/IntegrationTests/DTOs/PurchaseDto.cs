namespace BackendTest.Test.IntegrationTests;

/// <summary>
/// Test-only DTO used to deserialize purchase payloads in integration tests.
/// </summary>
public class PurchaseDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public List<PurchaseProductItemDto> Items { get; set; } = [];
}

public class PurchaseProductItemDto
{
    public int ProductId { get; set; }
    public int Count { get; set; }
}