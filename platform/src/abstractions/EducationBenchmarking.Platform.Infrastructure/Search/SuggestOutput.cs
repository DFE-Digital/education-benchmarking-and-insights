using System.Diagnostics.CodeAnalysis;
using Azure.Search.Documents.Models;

namespace EducationBenchmarking.Platform.Infrastructure.Search;

[ExcludeFromCodeCoverage]
public class SuggestOutput<T>
{
    public IEnumerable<SuggestValue<T>> Results { get; set; } = Array.Empty<SuggestValue<T>>();
}

[ExcludeFromCodeCoverage]
public class SuggestValue<T>
{
    public string? Text {get; set;}
    public T? Document {get; set;}

    public static SuggestValue<T> Create(SearchSuggestion<T> suggestion)
    {
        return new SuggestValue<T>
        {
            Text = suggestion.Text,
            Document = suggestion.Document
        };
    }
}
