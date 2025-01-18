using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Features.Census.Responses;

[ExcludeFromCodeCoverage]
public record CensusHistoryResponse
{
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public IEnumerable<CensusHistoryRowResponse> Rows { get; set; } = [];
}