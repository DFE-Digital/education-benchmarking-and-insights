using System.Diagnostics.CodeAnalysis;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace Web.App.Domain;

[ExcludeFromCodeCoverage]
public record LocalAuthorityStatisticalNeighbours
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public IEnumerable<LocalAuthorityStatisticalNeighbour>? StatisticalNeighbours { get; set; }
}

[ExcludeFromCodeCoverage]
public record LocalAuthorityStatisticalNeighbour
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public int? Order { get; set; }
}