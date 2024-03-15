using System.Diagnostics.CodeAnalysis;

namespace Web.App.Domain;

[ExcludeFromCodeCoverage]
public record ComparatorSet
{
    public int TotalResults { get; set; }
    public IEnumerable<string> Results { get; set; } = Array.Empty<string>();
}