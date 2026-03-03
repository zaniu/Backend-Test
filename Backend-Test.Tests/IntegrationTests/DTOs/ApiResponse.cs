namespace BackendTest.Test.IntegrationTests;

/// <summary>
/// Test-only wrapper that mirrors API response shape for integration-test JSON deserialization.
/// </summary>
public record ApiResponse<T>(T Value);