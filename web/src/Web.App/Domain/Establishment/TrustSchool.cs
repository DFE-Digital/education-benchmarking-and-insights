using System.Diagnostics.CodeAnalysis;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Web.App.Domain;

[ExcludeFromCodeCoverage]
public record TrustSchool
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? OverallPhase { get; set; }
}