using System.Diagnostics.CodeAnalysis;

namespace Web.App.Infrastructure.Session;

[ExcludeFromCodeCoverage]
public class CosmosCacheSettings
{
    public string? ConnectionString { get; set; }
    public bool IsDirect { get; set; } = true;
    public string? ContainerName { get; set; }
    public string? DatabaseName { get; set; }
}