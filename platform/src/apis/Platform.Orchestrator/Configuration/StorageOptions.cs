namespace Platform.Orchestrator.Configuration;

public class StorageOptions
{
    public const string SectionName = "StorageSettings";

    public BlobMoveTaskOptions LaaRiskFiles { get; init; } = new();
}

public class BlobMoveTaskOptions
{
    public string? SourceEndpoint { get; init; }
    public string? DestEndpoint { get; init; }
    public string? SourceContainer { get; init; }
    public string? DestContainer { get; init; }
    public bool IsValid =>
        !string.IsNullOrWhiteSpace(SourceEndpoint) &&
        !string.IsNullOrWhiteSpace(SourceContainer) &&
        !string.IsNullOrWhiteSpace(DestEndpoint) &&
        !string.IsNullOrWhiteSpace(DestContainer);
}
