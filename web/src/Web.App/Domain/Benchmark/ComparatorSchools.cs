using System.Diagnostics.CodeAnalysis;

namespace Web.App.Domain.Benchmark;

[ExcludeFromCodeCoverage]
public record ComparatorSchools
{
    public long? TotalSchools { get; set; }
    public IEnumerable<string> Schools { get; set; } = Array.Empty<string>();
}