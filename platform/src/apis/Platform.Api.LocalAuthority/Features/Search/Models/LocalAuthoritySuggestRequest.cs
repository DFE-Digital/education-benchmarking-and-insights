using System.Diagnostics.CodeAnalysis;
using Platform.Infrastructure;
using Platform.Search;

namespace Platform.Api.LocalAuthority.Features.Search.Models;

[ExcludeFromCodeCoverage]
public record LocalAuthoritySuggestRequest : SuggestRequest
{
    public override string SuggesterName => ResourceNames.Search.Suggesters.LocalAuthority;

    public string[] Exclude { get; set; } = [];
}