using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Features.Trusts.Models;

[ExcludeFromCodeCoverage]
public record TrustPhase
{
    public string? Phase { get; set; }
    public int? Count { get; set; }
}