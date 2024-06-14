using System.Diagnostics.CodeAnalysis;

namespace Web.App.Domain.Benchmark;

[ExcludeFromCodeCoverage]
public record SchoolComparatorSet
{
    public IEnumerable<string> Pupil { get; set; } = Array.Empty<string>();
    public IEnumerable<string> Building { get; set; } = Array.Empty<string>();
}

public record UserDefinedSchoolComparatorSet
{
    public string? RunId { get; set; }
    public long? TotalSchools { get; set; }
    public string[] Set { get; set; } = Array.Empty<string>();
}