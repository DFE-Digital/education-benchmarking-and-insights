using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Web.App.Domain;

[ExcludeFromCodeCoverage]
public record FederationSchool
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
}