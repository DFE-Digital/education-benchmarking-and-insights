using System.Collections.Generic;
using System.Linq;
using Platform.Api.LocalAuthority.Features.StatisticalNeighbours.Models;

namespace Platform.Api.LocalAuthority.Features.StatisticalNeighbours;

public static class Mapper
{
    public static StatisticalNeighboursResponse? MapToApiResponse(this IEnumerable<StatisticalNeighbourDto> data)
    {
        var models = data.ToArray();
        var first = models.FirstOrDefault();
        if (first?.LaCode == null)
        {
            return null;
        }

        var response = new StatisticalNeighboursResponse
        {
            Code = first.LaCode,
            Name = first.LaName,
            StatisticalNeighbours = models.Select(m => new StatisticalNeighbourResponse
            {
                Code = m.NeighbourLaCode,
                Name = m.NeighbourLaName,
                Position = m.NeighbourPosition
            }).ToArray()
        };

        return response;
    }
}