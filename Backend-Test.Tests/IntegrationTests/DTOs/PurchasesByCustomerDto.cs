namespace BackendTest.Test.IntegrationTests;

/// <summary>
/// Test-only DTO used to deserialize purchases-by-customer payloads in integration tests.
/// </summary>
public class PurchasesByCustomerDto
{
    public List<PurchaseDto> Purchases { get; set; } = [];
}
