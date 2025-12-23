using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Trust.Features.BudgetForecast.Models;

[ExcludeFromCodeCoverage]
public abstract record ItSpendingBase
{
    public decimal? Connectivity { get; set; }
    public decimal? OnsiteServers { get; set; }
    public decimal? ItLearningResources { get; set; }
    public decimal? AdministrationSoftwareAndSystems { get; set; }
    public decimal? LaptopsDesktopsAndTablets { get; set; }
    public decimal? OtherHardware { get; set; }
    public decimal? ItSupport { get; set; }
}

[ExcludeFromCodeCoverage]
public record ItSpendingResponse : ItSpendingBase
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
}


[ExcludeFromCodeCoverage]
public record ItSpendingForecastResponse : ItSpendingBase
{
    public int? Year { get; set; }
}