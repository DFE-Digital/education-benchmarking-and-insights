using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Features.Expenditure.Responses;

[ExcludeFromCodeCoverage]
public record ExpenditureHistoryRowResponse : ExpenditureResponse
{
    public int? Year { get; set; }
}