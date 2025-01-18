using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Features.Expenditure.Models;

[ExcludeFromCodeCoverage]
public record ExpenditureHistoryModel : ExpenditureModel
{
    public int? RunId { get; set; }
}