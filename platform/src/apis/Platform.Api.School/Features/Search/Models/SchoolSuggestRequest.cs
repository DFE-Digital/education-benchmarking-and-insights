using System.Collections.Generic;
using Platform.Domain;
using Platform.Infrastructure;
using Platform.Search;

namespace Platform.Api.School.Features.Search.Models;

public record SchoolSuggestRequest : SuggestRequest
{
    public override string SuggesterName => ResourceNames.Search.Suggesters.School;
    /// <summary>A collection of Unique Reference Numbers (URNs) to exclude from the suggestions.</summary>
    public string[] Exclude { get; set; } = [];
    /// <summary>Whether to exclude schools that are missing financial data.</summary>
    public bool ExcludeMissingFinancialData { get; set; }

    public string FilterExpression() => new List<string>()
        .NotValuesFilter("URN", Exclude.Length > 0
            ? new CharacteristicList
            {
                Values = Exclude
            }
            : null)
        .ConditionalExpressionFilter(ExcludeMissingFinancialData, x => x.NotNullValueFilter("PeriodCoveredByReturn"))
        .BuildFilter();
}