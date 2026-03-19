using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Web.App.Domain.LocalAuthorities;

[ExcludeFromCodeCoverage]
public record LocalAuthoritySchool
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? OverallPhase { get; set; }
}