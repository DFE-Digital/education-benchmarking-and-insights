using System;

namespace Platform.Orchestrator.Configuration;

// TODO: rider inspection warning
public class StorageOptions
{
    public const string SectionName = "StorageSettings";

    public Uri? SourceAccountUri { get; set; }
    public Uri? DestAccountUri { get; set; }
    public string? SourceContainer { get; set; }
    public string? DestContainer { get; set; }
}
