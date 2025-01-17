using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable InconsistentNaming
namespace Platform.Api.Establishment.Features.LocalAuthorities;

[ExcludeFromCodeCoverage]
public record LocalAuthority
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public IEnumerable<LocalAuthoritySchool>? Schools { get; set; }
}

[ExcludeFromCodeCoverage]
public record LocalAuthoritySchool
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? OverallPhase { get; set; }

    public static readonly string[] Fields = [nameof(URN), nameof(SchoolName), nameof(OverallPhase)];
}