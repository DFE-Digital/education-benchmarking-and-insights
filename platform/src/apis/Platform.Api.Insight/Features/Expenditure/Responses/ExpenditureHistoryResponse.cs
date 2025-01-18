using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Features.Expenditure.Responses;

[ExcludeFromCodeCoverage]
public record ExpenditureHistoryResponse
{
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public IEnumerable<ExpenditureHistoryRowResponse> Rows { get; set; } = [];
}