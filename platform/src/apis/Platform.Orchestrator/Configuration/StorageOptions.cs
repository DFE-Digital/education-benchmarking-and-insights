// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Platform.Orchestrator.Configuration;

public class StorageOptions
{
    public const string SectionName = "StorageSettings";

    public BlobMoveTaskOptions LaaRiskFiles { get; set; } = new();
}

public class BlobMoveTaskOptions
{
    public string? SourceEndpoint { get; set; }
    public string? DestEndpoint { get; set; }
    public string? SourceContainer { get; set; }
    public string? DestContainer { get; set; }
    public bool IsValid =>
        !string.IsNullOrWhiteSpace(SourceEndpoint) &&
        !string.IsNullOrWhiteSpace(SourceContainer) &&
        !string.IsNullOrWhiteSpace(DestEndpoint) &&
        !string.IsNullOrWhiteSpace(DestContainer);
}
