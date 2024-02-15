using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

[ExcludeFromCodeCoverage]
public record SuggestOutput<T>
{
    public IEnumerable<SuggestValue<T>> Results { get; set; } = Array.Empty<SuggestValue<T>>();
}

[ExcludeFromCodeCoverage]
public record SuggestValue<T>
{
    public string? Text {get; set;}
    public T? Document {get; set;}
}