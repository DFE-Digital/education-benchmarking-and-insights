using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Establishment.LocalAuthorities;

[ExcludeFromCodeCoverage]
public record LocalAuthority
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public IEnumerable<LocalAuthoritySchool>? Schools { get; set; }
}

public record LocalAuthoritySchool
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? OverallPhase { get; set; }
}