using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Features.ItSpend.Responses;

[ExcludeFromCodeCoverage]
public record ItSpendTrustResponse : ItSpendResponse
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
}