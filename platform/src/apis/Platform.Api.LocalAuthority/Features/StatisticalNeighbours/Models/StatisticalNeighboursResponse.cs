// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthority.Features.StatisticalNeighbours.Models;

[ExcludeFromCodeCoverage]
public record StatisticalNeighboursResponse
{
    public string? Code { get; init; }
    public string? Name { get; init; }
    public StatisticalNeighbourResponse[]? StatisticalNeighbours { get; init; }
}

[ExcludeFromCodeCoverage]
public record StatisticalNeighbourResponse
{
    public string? Code { get; init; }
    public string? Name { get; init; }
    public int? Position { get; init; }
}