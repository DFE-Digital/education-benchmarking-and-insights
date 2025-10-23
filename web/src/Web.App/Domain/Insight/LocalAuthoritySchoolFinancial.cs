// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Web.App.Domain;

public class LocalAuthoritySchoolFinancial
{
    public string? Urn { get; set; }
    public string? SchoolName { get; set; }
    public int? PeriodCoveredByReturn { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? TotalExpenditure { get; set; }
    public decimal? TotalTeachingSupportStaffCosts { get; set; }
    public decimal? RevenueReserve { get; set; }
}