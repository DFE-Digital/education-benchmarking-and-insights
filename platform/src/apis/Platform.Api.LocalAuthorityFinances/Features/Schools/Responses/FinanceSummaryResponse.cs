// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Platform.Api.LocalAuthorityFinances.Features.Schools.Responses;

public record FinanceSummaryResponse
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? TotalExpenditure { get; set; }
    public decimal? TotalTeachingSupportStaffCosts { get; set; }
    public decimal? RevenueReserve { get; set; }
    public int? PeriodCoveredByReturn { get; set; }
}