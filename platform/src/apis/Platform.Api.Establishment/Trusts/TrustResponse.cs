using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Platform.Api.Establishment.Schools;
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Platform.Api.Establishment.Trusts;

public static class TrustResponseFactory
{
    public static TrustResponse Create(Trust trust, IEnumerable<School> schools)
    {
        return new TrustResponse
        {
            CompanyNumber = trust.CompanyNumber,
            TrustName = trust.TrustName,
            Schools = schools.Select(s => new TrustSchoolModel
            {
                URN = s.URN,
                SchoolName = s.SchoolName,
                OverallPhase = s.OverallPhase
            }).ToArray()
        };
    }
}

[ExcludeFromCodeCoverage]
public record TrustResponse
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
    public TrustSchoolModel[] Schools { get; set; } = [];
}

[ExcludeFromCodeCoverage]
public record TrustSchoolModel
{
    // ReSharper disable once InconsistentNaming
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? OverallPhase { get; set; }
}