using System.Diagnostics.CodeAnalysis;

namespace Web.App.Domain;

[ExcludeFromCodeCoverage]
public record ComparatorSet
{
    public IEnumerable<string> DefaultPupil { get; set; } = Array.Empty<string>();
    public IEnumerable<string> DefaultArea { get; set; } = Array.Empty<string>();
}