using System.Collections.Generic;
using Platform.Domain;
using Platform.Infrastructure;
using Platform.Search;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Platform.Api.Establishment.Features.Trusts.Requests;

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