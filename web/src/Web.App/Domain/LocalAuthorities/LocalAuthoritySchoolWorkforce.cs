// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Web.App.Domain;

public class LocalAuthoritySchoolWorkforce
{
    public string? Urn { get; set; }
    public string? SchoolName { get; set; }
    public int? PeriodCoveredByReturn { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? PupilTeacherRatio { get; set; }
    public decimal? EhcPlan { get; set; }
    public decimal? SenSupport { get; set; }
}