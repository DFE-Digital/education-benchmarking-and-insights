using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Features.Census.Responses;

[ExcludeFromCodeCoverage]
public record CensusHistoryRowResponse : CensusResponse
{
    public int? Year { get; set; }
}