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

public record LocalAuthoritySchoolResponseBase
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
}

public record LocalAuthoritySchoolResponse : LocalAuthoritySchoolResponseBase
{
    public static readonly string[] Fields = [nameof(URN), nameof(SchoolName), nameof(OverallPhase)];
    public string? OverallPhase { get; set; }
}

public record LocalAuthoritySchoolFinanceSummaryResponse : LocalAuthoritySchoolResponseBase
{
    public decimal? TotalPupils { get; set; }
    public decimal? TotalExpenditure { get; set; }
    public decimal? TotalTeachingSupportStaffCosts { get; set; }
    public decimal? RevenueReserve { get; set; }
    public int? PeriodCoveredByReturn { get; set; }
}


public record LocalAuthoritySchoolWorkforceSummaryResponse : LocalAuthoritySchoolResponseBase
{
    public decimal? TotalPupils { get; set; }
    public decimal? PupilTeacherRatio { get; set; }
    public decimal? EHCPlan { get; set; }
    public decimal? SENSupport { get; set; }
}