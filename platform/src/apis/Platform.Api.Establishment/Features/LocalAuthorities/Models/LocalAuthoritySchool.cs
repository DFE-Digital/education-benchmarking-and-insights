using System.Diagnostics.CodeAnalysis;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable InconsistentNaming
namespace Platform.Api.Establishment.Features.LocalAuthorities.Models;

[ExcludeFromCodeCoverage]
public record LocalAuthoritySchool
{
    public static readonly string[] Fields = [nameof(URN), nameof(SchoolName), nameof(OverallPhase)];
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? OverallPhase { get; set; }
}