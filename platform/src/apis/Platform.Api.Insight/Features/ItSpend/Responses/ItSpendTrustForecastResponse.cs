using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Features.ItSpend.Responses;

[ExcludeFromCodeCoverage]
public record ItSpendTrustForecastResponse
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
    public ItSpendTrustForecastYear[]? Years { get; set; }
}

public record ItSpendTrustForecastYear : ItSpendResponse
{
    public int? Year { get; set; }
}