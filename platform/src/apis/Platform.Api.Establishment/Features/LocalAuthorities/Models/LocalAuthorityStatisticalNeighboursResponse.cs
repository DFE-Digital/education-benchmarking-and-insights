using System.Linq;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Platform.Api.Establishment.Features.LocalAuthorities.Models;

public record LocalAuthorityStatisticalNeighboursResponse
{
    public string? Code { get; init; }
    public string? Name { get; init; }
    public LocalAuthorityStatisticalNeighbourResponse[]? StatisticalNeighbours { get; init; }
}

public record LocalAuthorityStatisticalNeighbourResponse
{
    public string? Code { get; init; }
    public string? Name { get; init; }
    public int? Position { get; init; }
}

public static class LocalAuthorityStatisticalNeighbourResponseFactory
{
    public static LocalAuthorityStatisticalNeighboursResponse? Create(LocalAuthorityStatisticalNeighbour[] models)
    {
        var first = models.FirstOrDefault();
        if (first?.LaCode == null)
        {
            return null;
        }

        var response = new LocalAuthorityStatisticalNeighboursResponse
        {
            Code = first.LaCode,
            Name = first.LaName,
            StatisticalNeighbours = models.Select(m => new LocalAuthorityStatisticalNeighbourResponse
            {
                Code = m.NeighbourLaCode,
                Name = m.NeighbourLaName,
                Position = m.NeighbourPosition
            }).ToArray()
        };

        return response;
    }
}