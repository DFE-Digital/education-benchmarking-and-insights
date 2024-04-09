using System.Diagnostics.CodeAnalysis;

namespace Web.App.Infrastructure.Redis;

[ExcludeFromCodeCoverage]
public record Settings
{
    public string? ConnectionString { get; set; }
    public string? InstanceName  { get; set; }
}