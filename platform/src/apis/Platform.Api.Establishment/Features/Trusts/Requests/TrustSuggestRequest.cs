using System.Diagnostics.CodeAnalysis;
using Platform.Infrastructure;
using Platform.Search;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace Platform.Api.Establishment.Features.Trusts.Requests;

[ExcludeFromCodeCoverage]
public record TrustSuggestRequest : SuggestRequest
{
    public override string SuggesterName => ResourceNames.Search.Suggesters.Trust;

    public string[] Exclude { get; set; } = [];
}