using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global

namespace Platform.Api.Establishment.Features.LocalAuthorities.Models;

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