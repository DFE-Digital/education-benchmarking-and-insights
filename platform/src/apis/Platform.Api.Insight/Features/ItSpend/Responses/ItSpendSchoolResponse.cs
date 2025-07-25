using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Features.ItSpend.Responses;

[ExcludeFromCodeCoverage]
public record ItSpendSchoolResponse : ItSpendResponse
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
    public int? PeriodCoveredByReturn { get; set; }
    public decimal? TotalPupils { get; set; }
}