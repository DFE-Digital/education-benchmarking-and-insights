using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Features.Income.Models;

[ExcludeFromCodeCoverage]
public record IncomeHistoryModel : IncomeModel
{
    public int? RunId { get; set; }
}