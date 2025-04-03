using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Platform.Domain;
using Platform.Infrastructure;
using Platform.Search;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Platform.Api.Establishment.Features.Schools.Requests;

[ExcludeFromCodeCoverage]
public record SchoolSuggestRequest : SuggestRequest
{
    public override string SuggesterName => ResourceNames.Search.Suggesters.School;
    public string[] Exclude { get; set; } = [];
    public bool ExcludeMissingFinancialData { get; set; } = false;

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