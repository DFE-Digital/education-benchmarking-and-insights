using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.School.Features.Accounts.Models;

[ExcludeFromCodeCoverage]
public record ItSpendingResponse
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
    public int? PeriodCoveredByReturn { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? Connectivity { get; set; }
    public decimal? OnsiteServers { get; set; }
    public decimal? ItLearningResources { get; set; }
    public decimal? AdministrationSoftwareAndSystems { get; set; }
    public decimal? LaptopsDesktopsAndTablets { get; set; }
    public decimal? OtherHardware { get; set; }
    public decimal? ItSupport { get; set; }
}