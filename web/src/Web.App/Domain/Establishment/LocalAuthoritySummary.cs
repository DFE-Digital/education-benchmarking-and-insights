using System.Diagnostics.CodeAnalysis;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Web.App.Domain;

[ExcludeFromCodeCoverage]
public record LocalAuthoritySummary
{
    public string? Code { get; set; }
    public string? Name { get; set; }
}