using System.Diagnostics.CodeAnalysis;

namespace Web.App.Domain;

[ExcludeFromCodeCoverage]
public record ComparatorTrusts
{
    public long? TotalTrusts { get; set; }
    public IEnumerable<string> Trusts { get; set; } = [];
}