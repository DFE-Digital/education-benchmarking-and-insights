using System;
using System.Linq;

// ReSharper disable InconsistentNaming

namespace Platform.Api.LocalAuthority.Features.Details.Models;


public static class FinanceSummarySortFields
{
    public const string SchoolName = nameof(LocalAuthoritySchoolFinanceSummaryResponse.SchoolName);
    public const string TotalPupils = nameof(LocalAuthoritySchoolFinanceSummaryResponse.TotalPupils);
    public const string TotalExpenditure = nameof(LocalAuthoritySchoolFinanceSummaryResponse.TotalExpenditure);
    public const string TotalTeachingSupportStaffCosts = nameof(LocalAuthoritySchoolFinanceSummaryResponse.TotalTeachingSupportStaffCosts);
    public const string RevenueReserve = nameof(LocalAuthoritySchoolFinanceSummaryResponse.RevenueReserve);

    public static readonly string[] All =
    [
        SchoolName,
        TotalPupils,
        TotalExpenditure,
        TotalTeachingSupportStaffCosts,
        RevenueReserve
    ];

    public static bool IsValid(string? type) => All.Any(a => a.Equals(type, StringComparison.OrdinalIgnoreCase));
}

public static class WorkforceSummarySortFields
{
    public const string SchoolName = nameof(LocalAuthoritySchoolWorkforceSummaryResponse.SchoolName);
    public const string TotalPupils = nameof(LocalAuthoritySchoolWorkforceSummaryResponse.TotalPupils);
    public const string PupilTeacherRatio = nameof(LocalAuthoritySchoolWorkforceSummaryResponse.PupilTeacherRatio);
    public const string EHCPlan = nameof(LocalAuthoritySchoolWorkforceSummaryResponse.EHCPlan);
    public const string SENSupport = nameof(LocalAuthoritySchoolWorkforceSummaryResponse.SENSupport);

    public static readonly string[] All =
    [
        SchoolName,
        TotalPupils,
        PupilTeacherRatio,
        EHCPlan,
        SENSupport
    ];

    public static bool IsValid(string? type) => All.Any(a => a.Equals(type, StringComparison.OrdinalIgnoreCase));
}