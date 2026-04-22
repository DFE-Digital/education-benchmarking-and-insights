using System.Diagnostics.CodeAnalysis;
using Platform.Infrastructure;
using Platform.Search;

namespace Platform.Api.LocalAuthority.Features.Search.Models;

/// <summary>
/// Represents a local authority suggest request.
/// </summary>
[ExcludeFromCodeCoverage]
public record LocalAuthoritySuggestRequest : SuggestRequest
{
    /// <summary>
    /// The name of the suggester to use.
    /// </summary>
    public override string SuggesterName => ResourceNames.Search.Suggesters.LocalAuthority;

    /// <summary>
    /// A list of local authority codes to exclude from the suggestions.
    /// </summary>
    public string[] Exclude { get; set; } = [];
}