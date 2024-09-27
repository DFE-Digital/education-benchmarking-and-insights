using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Platform.Api.Establishment.Schools;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Platform.Api.Establishment.LocalAuthorities;

public static class LocalAuthorityResponseFactory
{
    public static LocalAuthorityResponse Create(LocalAuthority localAuthority, IEnumerable<School> schools)
    {
        return new LocalAuthorityResponse
        {
            Code = localAuthority.Code,
            Name = localAuthority.Name,
            Schools = schools.Select(s => new LocalAuthoritySchoolModel
            {
                URN = s.URN,
                SchoolName = s.SchoolName,
                OverallPhase = s.OverallPhase
            }).ToArray()
        };
    }
}

[ExcludeFromCodeCoverage]
public record LocalAuthorityResponse
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public LocalAuthoritySchoolModel[] Schools { get; set; } = [];
}

[ExcludeFromCodeCoverage]
public record LocalAuthoritySchoolModel
{
    // ReSharper disable once InconsistentNaming
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? OverallPhase { get; set; }
}