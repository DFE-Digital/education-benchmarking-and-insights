using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Features.ItSpend.Models;

[ExcludeFromCodeCoverage]
public record ItSpendTrustForecastModel
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
    public int Year { get; set; }
    public decimal? Connectivity { get; set; }
    public decimal? OnsiteServers { get; set; }
    public decimal? ItLearningResources { get; set; }
    public decimal? AdministrationSoftwareAndSystems { get; set; }
    public decimal? LaptopsDesktopsAndTablets { get; set; }
    public decimal? OtherHardware { get; set; }
    public decimal? ItSupport { get; set; }
}