using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Features.ItSpend.Responses;

[ExcludeFromCodeCoverage]

public record ItSpendTrustForecastResponse : ItSpendResponse
{
    public int? Year { get; set; }
}