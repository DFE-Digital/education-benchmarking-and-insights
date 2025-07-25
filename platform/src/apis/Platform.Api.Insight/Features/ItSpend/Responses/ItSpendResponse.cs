using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Features.ItSpend.Responses;

[ExcludeFromCodeCoverage]
public abstract record ItSpendResponse
{
    public decimal? Connectivity { get; set; }
    public decimal? OnsiteServers { get; set; }
    public decimal? ItLearningResources { get; set; }
    public decimal? AdministrationSoftwareAndSystems { get; set; }
    public decimal? LaptopsDesktopsAndTablets { get; set; }
    public decimal? OtherHardware { get; set; }
    public decimal? ItSupport { get; set; }
}