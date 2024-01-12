using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Web.Domain;

[ExcludeFromCodeCoverage]
public class ComparatorSet<T>
{
    public int TotalResults { get; set; }
    public IEnumerable<T> Results { get; set; } = Array.Empty<T>();
}