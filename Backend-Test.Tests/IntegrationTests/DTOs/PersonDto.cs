namespace BackendTest.Test.IntegrationTests;

/// <summary>
/// Test-only DTO used to deserialize person payloads in integration tests.
/// </summary>
public class PersonDto
{
    public int Id { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public int YearOfBirth { get; set; }
}