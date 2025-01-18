using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Features.Income.Responses;

[ExcludeFromCodeCoverage]
public record IncomeHistoryResponse
{
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public IEnumerable<IncomeHistoryRowResponse> Rows { get; set; } = [];
}