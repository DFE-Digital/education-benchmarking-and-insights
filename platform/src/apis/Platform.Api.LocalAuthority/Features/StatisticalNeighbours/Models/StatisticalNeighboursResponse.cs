// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Platform.Api.LocalAuthority.Features.StatisticalNeighbours.Models;

public record StatisticalNeighboursResponse
{
    public string? Code { get; init; }
    public string? Name { get; init; }
    public StatisticalNeighbourResponse[]? StatisticalNeighbours { get; init; }
}

public record StatisticalNeighbourResponse
{
    public string? Code { get; init; }
    public string? Name { get; init; }
    public int? Position { get; init; }
}