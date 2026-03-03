namespace BackendTest.Model;

public class EnvironmentConfiguration
{
    public string ApiVersion { get; set; } = string.Empty;
    public string UiVersion { get; set; } = string.Empty;

    public bool IsProduction { get; set; } = false;
}
