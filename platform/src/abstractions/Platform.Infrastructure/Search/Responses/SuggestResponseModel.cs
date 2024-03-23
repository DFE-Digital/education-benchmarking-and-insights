using System.Diagnostics.CodeAnalysis;
using Azure.Search.Documents.Models;

namespace Platform.Infrastructure.Search;

[ExcludeFromCodeCoverage]
public record SuggestResponseModel<T>
{
    public IEnumerable<SuggestValueResponseModel<T>> Results { get; set; } = Array.Empty<SuggestValueResponseModel<T>>();
}

[ExcludeFromCodeCoverage]
public record SuggestValueResponseModel<T>
{
    public string? Text { get; set; }
    public T? Document { get; set; }

    public static SuggestValueResponseModel<T> Create(SearchSuggestion<T> suggestion)
    {
        return new SuggestValueResponseModel<T>
        {
            Text = suggestion.Text,
            Document = suggestion.Document
        };
    }
}
