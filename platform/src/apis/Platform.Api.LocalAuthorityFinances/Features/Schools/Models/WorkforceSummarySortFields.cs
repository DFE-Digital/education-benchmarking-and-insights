using System;
using System.Linq;
using Platform.Api.LocalAuthorityFinances.Features.Schools.Responses;
// ReSharper disable InconsistentNaming

namespace Platform.Api.LocalAuthorityFinances.Features.Schools.Models;

public static class WorkforceSummarySortFields
{
    public const string SchoolName = nameof(WorkforceSummaryResponse.SchoolName);
    public const string TotalPupils = nameof(WorkforceSummaryResponse.TotalPupils);
    public const string PupilTeacherRatio = nameof(WorkforceSummaryResponse.PupilTeacherRatio);
    public const string EHCPlan = nameof(WorkforceSummaryResponse.EHCPlan);
    public const string SENSupport = nameof(WorkforceSummaryResponse.SENSupport);

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