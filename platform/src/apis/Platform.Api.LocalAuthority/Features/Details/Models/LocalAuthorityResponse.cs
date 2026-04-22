using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthority.Features.Details.Models;

/// <summary>
/// Represents the details of a local authority.
/// </summary>
[ExcludeFromCodeCoverage]
public record LocalAuthorityResponse
{
    /// <summary>
    /// The three-digit local authority code.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// The name of the local authority.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The list of schools associated with the local authority.
    /// </summary>
    public IEnumerable<LocalAuthoritySchoolResponse>? Schools { get; set; }

    /// <summary>
    /// The headline statistics for the local authority.
    /// </summary>
    public LocalAuthorityHeadlineStatisticsResponse? HeadlineStatistics { get; set; }
}

/// <summary>
/// Base properties for a local authority school response.
/// </summary>
[ExcludeFromCodeCoverage]
public record LocalAuthoritySchoolResponseBase
{
    /// <summary>
    /// The Unique Reference Number (URN) of the school.
    /// </summary>
    public string? URN { get; set; }

    /// <summary>
    /// The name of the school.
    /// </summary>
    public string? SchoolName { get; set; }
}

/// <summary>
/// Represents the headline statistics for a local authority.
/// </summary>
public record LocalAuthorityHeadlineStatisticsResponse
{
    /// <summary>
    /// The Dedicated Schools Grant (DSG) High Needs allocation.
    /// </summary>
    public decimal? DSGHighNeedsAllocation { get; set; }

    /// <summary>
    /// The total high needs outturn.
    /// </summary>
    public decimal? OutturnTotalHighNeeds { get; set; }

    /// <summary>
    /// The outturn DSG carried forward.
    /// </summary>
    public decimal? OutturnDSGCarriedForward { get; set; }

    /// <summary>
    /// The outturn DSG carried forward from the previous period.
    /// </summary>
    public decimal? OutturnDSGCarriedForwardPreviousPeriod { get; set; }
}

/// <summary>
/// Represents a basic school response within a local authority.
/// </summary>
[ExcludeFromCodeCoverage]
public record LocalAuthoritySchoolResponse : LocalAuthoritySchoolResponseBase
{
    /// <summary>
    /// Internal array of available fields.
    /// </summary>
    public static readonly string[] Fields = [nameof(URN), nameof(SchoolName), nameof(OverallPhase)];

    /// <summary>
    /// The overall phase of the school.
    /// </summary>
    public string? OverallPhase { get; set; }
}

/// <summary>
/// Represents a finance summary for a school within a local authority.
/// </summary>
[ExcludeFromCodeCoverage]
public record LocalAuthoritySchoolFinanceSummaryResponse : LocalAuthoritySchoolResponseBase
{
    /// <summary>
    /// The total number of pupils.
    /// </summary>
    public decimal? TotalPupils { get; set; }

    /// <summary>
    /// The total expenditure.
    /// </summary>
    public decimal? TotalExpenditure { get; set; }

    /// <summary>
    /// The total costs for teaching support staff.
    /// </summary>
    public decimal? TotalTeachingSupportStaffCosts { get; set; }

    /// <summary>
    /// The revenue reserve balance.
    /// </summary>
    public decimal? RevenueReserve { get; set; }

    /// <summary>
    /// The number of months covered by the return period.
    /// </summary>
    public int? PeriodCoveredByReturn { get; set; }
}

/// <summary>
/// Represents a workforce summary for a school within a local authority.
/// </summary>
[ExcludeFromCodeCoverage]
public record LocalAuthoritySchoolWorkforceSummaryResponse : LocalAuthoritySchoolResponseBase
{
    /// <summary>
    /// The total number of pupils.
    /// </summary>
    public decimal? TotalPupils { get; set; }

    /// <summary>
    /// The pupil to teacher ratio.
    /// </summary>
    public decimal? PupilTeacherRatio { get; set; }

    /// <summary>
    /// The percentage or number of pupils with an Education, Health and Care (EHC) plan.
    /// </summary>
    public decimal? EHCPlan { get; set; }

    /// <summary>
    /// The percentage or number of pupils receiving SEN support.
    /// </summary>
    public decimal? SENSupport { get; set; }
}