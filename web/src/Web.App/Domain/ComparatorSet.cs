using System.Diagnostics.CodeAnalysis;

namespace Web.App.Domain;

[ExcludeFromCodeCoverage]
public class ComparatorSet<T>
{
    public int TotalResults { get; set; }
    public IEnumerable<T> Results { get; set; } = Array.Empty<T>();
}