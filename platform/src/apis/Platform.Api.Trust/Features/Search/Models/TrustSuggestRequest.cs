using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Platform.Domain;
using Platform.Infrastructure;
using Platform.Search;

namespace Platform.Api.Trust.Features.Search.Models;

[ExcludeFromCodeCoverage]
public record TrustSuggestRequest : SuggestRequest
{
    public override string SuggesterName => ResourceNames.Search.Suggesters.Trust;

    public string[] Exclude { get; set; } = [];

    public string FilterExpression() => new List<string>()
        .NotValuesFilter("CompanyNumber", Exclude.Length > 0
            ? new CharacteristicList
            {
                Values = Exclude
            }
            : null)
        .BuildFilter();
}