// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Platform.Orchestrator.Configuration;

public class StorageOptions
{
    public const string SectionName = "StorageSettings";

    public BlobMoveTaskOptions LaaRiskFiles { get; set; } = new();
}

public class BlobMoveTaskOptions
{
    public string? SourceConnectionString { get; set; }
    public string? DestConnectionString { get; set; }
    public string? SourceContainer { get; set; }
    public string? DestContainer { get; set; }
    public bool IsValid =>
        !string.IsNullOrWhiteSpace(SourceConnectionString) &&
        !string.IsNullOrWhiteSpace(SourceContainer) &&
        !string.IsNullOrWhiteSpace(DestConnectionString) &&
        !string.IsNullOrWhiteSpace(DestContainer);
}
