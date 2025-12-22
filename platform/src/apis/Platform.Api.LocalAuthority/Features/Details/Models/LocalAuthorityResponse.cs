using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthority.Features.Details.Models;

[ExcludeFromCodeCoverage]
public record LocalAuthorityResponse
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public IEnumerable<LocalAuthoritySchoolResponse>? Schools { get; set; }
}

public record LocalAuthoritySchoolResponse
{
    public static readonly string[] Fields = [nameof(URN), nameof(SchoolName), nameof(OverallPhase)];
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? OverallPhase { get; set; }
}