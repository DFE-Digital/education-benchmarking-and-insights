using System.Diagnostics.CodeAnalysis;
using Platform.Infrastructure;
using Platform.Search;

namespace Platform.Api.Establishment.Features.Schools.Requests;

[ExcludeFromCodeCoverage]
public record SchoolSuggestRequest : SuggestRequest
{
    public override string SuggesterName => ResourceNames.Search.Suggesters.School;

    public string[] Exclude { get; set; } = [];
}