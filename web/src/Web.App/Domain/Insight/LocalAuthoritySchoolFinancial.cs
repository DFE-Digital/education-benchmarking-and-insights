// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Web.App.Domain;

public class LocalAuthoritySchoolFinancial
{
    public string? Urn { get; set; }
    public string? SchoolName { get; set; }
    public int? PeriodCoveredByReturn { get; set; }
    public decimal? TotalPupils { get; set; }
}