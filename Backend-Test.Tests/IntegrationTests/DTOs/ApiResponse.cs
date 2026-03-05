namespace BackendTest.Test.IntegrationTests;

/// <summary>
/// Test-only wrapper that mirrors API response shape for integration-test JSON deserialization.
/// </summary>
public class ApiResponse<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public T Value { get; set; }
    public List<string> Errors { get; set; }
    public string TraceId { get; set; }
}
