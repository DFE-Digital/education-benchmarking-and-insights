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

}