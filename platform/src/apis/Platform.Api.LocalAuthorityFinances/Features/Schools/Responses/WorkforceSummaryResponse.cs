// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable InconsistentNaming

namespace Platform.Api.LocalAuthorityFinances.Features.Schools.Responses;

public record WorkforceSummaryResponse
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? PupilTeacherRatio { get; set; }
    public decimal? EHCPlan { get; set; }
    public decimal? SENSupport { get; set; }
    public int? PeriodCoveredByReturn { get; set; }
}