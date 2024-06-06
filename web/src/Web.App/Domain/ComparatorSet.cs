using System.Diagnostics.CodeAnalysis;
namespace Web.App.Domain;

[ExcludeFromCodeCoverage]
public record ComparatorSet
{
    public IEnumerable<string> Pupil { get; set; } = Array.Empty<string>();
    public IEnumerable<string> Building { get; set; } = Array.Empty<string>();
}

public record ComparatorSetUserDefined
{
    public string? RunId { get; set; }
    public long? TotalSchools { get; set; }
    public string[] Set { get; set; } = Array.Empty<string>();
}