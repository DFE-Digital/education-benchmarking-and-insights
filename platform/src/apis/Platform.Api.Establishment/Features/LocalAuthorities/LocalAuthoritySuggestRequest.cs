using System.Diagnostics.CodeAnalysis;
using Platform.Infrastructure;
using Platform.Search;

namespace Platform.Api.Establishment.Features.LocalAuthorities;

[ExcludeFromCodeCoverage]
public record LocalAuthoritySuggestRequest : SuggestRequest
{
    public override string SuggesterName => ResourceNames.Search.Suggesters.LocalAuthority;

    public string[] Exclude { get; set; } = [];
}