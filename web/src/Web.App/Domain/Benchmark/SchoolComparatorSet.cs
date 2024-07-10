using System.Diagnostics.CodeAnalysis;

namespace Web.App.Domain;

[ExcludeFromCodeCoverage]
public record SchoolComparatorSet
{
    public string[] Pupil { get; set; } = [];
    public string[] Building { get; set; } = [];
}

public record UserDefinedSchoolComparatorSet
{
    public string? RunId { get; set; }
    public long? TotalSchools { get; set; }
    public string[] Set { get; set; } = Array.Empty<string>();
}