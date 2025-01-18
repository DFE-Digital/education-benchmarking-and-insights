using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Features.Income.Responses;

[ExcludeFromCodeCoverage]
public record IncomeHistoryRowResponse : IncomeResponse
{
    public int? Year { get; set; }
}