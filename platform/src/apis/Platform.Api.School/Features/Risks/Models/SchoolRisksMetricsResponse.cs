using System.Diagnostics.CodeAnalysis;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Platform.Api.School.Features.Risks.Models;

[ExcludeFromCodeCoverage]
public record SchoolRisksMetricsResponse
{
    public string Urn { get; init; } = string.Empty;
    public string RiskGroup { get; init; } = string.Empty;
    public string RiskIndicator { get; init; } = string.Empty;
    public decimal RiskIndicatorValue { get; init; }
    public string RiskIndicatorFlag { get; init; } = string.Empty;
    public decimal RiskIndicatorContribution { get; init; }
    public decimal RiskIndicatorContributionMax { get; init; }
}
