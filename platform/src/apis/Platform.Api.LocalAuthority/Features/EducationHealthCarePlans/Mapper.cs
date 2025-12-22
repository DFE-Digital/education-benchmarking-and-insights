using System.Collections.Generic;
using System.Linq;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Models;

namespace Platform.Api.LocalAuthority.Features.EducationHealthCarePlans;

public static class Mapper
{
    public static IEnumerable<EducationHealthCarePlansResponse> MapToApiResponse(this IEnumerable<EducationHealthCarePlansDto> values)
    {
        return values.ToArray().Select(MapToApiResponse);
    }

    private static EducationHealthCarePlansResponse MapToApiResponse(EducationHealthCarePlansDto value) => new()
    {
        Code = value.LaCode,
        Name = value.Name,
        Population2To18 = value.Population2To18,
        TotalPupils = value.TotalPupils,
        Total = value.Total,
        Mainstream = value.Mainstream,
        Resourced = value.Resourced,
        Special = value.Special,
        Independent = value.Independent,
        Hospital = value.Hospital,
        Post16 = value.Post16,
        Other = value.Other
    };

    public static EducationHealthCarePlansYearHistory MapToApiResponse(this YearsModelDto years, IEnumerable<EducationHealthCarePlansYearDto> models)
    {
        return new EducationHealthCarePlansYearHistory
        {
            StartYear = years.StartYear,
            EndYear = years.EndYear,
            Plans = models.Select(x => x.MapToApiResponse()).ToArray()
        };
    }

    private static EducationHealthCarePlansYearResponse MapToApiResponse(this EducationHealthCarePlansYearDto value)
    {
        int? year = null;
        if (int.TryParse(value.RunId, out var parsed))
        {
            year = parsed;
        }

        return new EducationHealthCarePlansYearResponse
        {
            Code = value.LaCode,
            Name = value.Name,
            Year = year,
            Total = value.Total,
            Mainstream = value.Mainstream,
            Resourced = value.Resourced,
            Special = value.Special,
            Independent = value.Independent,
            Hospital = value.Hospital,
            Post16 = value.Post16,
            Other = value.Other
        };
    }
}