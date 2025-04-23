using Web.App.Controllers.Api.Responses;
using Web.App.Domain.NonFinancial;

namespace Web.App.Controllers.Api.Mappers;

public static class EducationHealthCarePlansResponseMapper
{
    public static IEnumerable<EducationHealthCarePlansComparisonResponse> MapToApiResponse(this LocalAuthorityNumberOfPlans[] plans)
    {
        return plans.Select(item => new EducationHealthCarePlansComparisonResponse
        {
            Code = item.Code,
            Name = item.Name,
            Population2To18 = item.Population2To18,
            Total = item.Total,
            Mainstream = item.Mainstream,
            Resourced = item.Resourced,
            Special = item.Special,
            Independent = item.Independent,
            Hospital = item.Hospital,
            Post16 = item.Post16,
            Other = item.Other
        });
    }

    public static IEnumerable<EducationHealthCarePlansHistoryResponse> MapToApiResponse(this EducationHealthCarePlansHistory<LocalAuthorityNumberOfPlansYear>? history, string code)
    {
        if (history?.StartYear == null || history.EndYear == null)
        {
            yield break;
        }

        for (var year = history.StartYear.Value; year <= history.EndYear; year++)
        {
            var item = history.Plans?
                .Where(x => x.Year == year)
                .SingleOrDefault(x => x.Code == code);

            if (item == null)
            {
                yield return new EducationHealthCarePlansHistoryResponse
                {
                    Year = year,
                    Term = $"{year - 1} to {year}"
                };
            }
            else
            {
                yield return new EducationHealthCarePlansHistoryResponse
                {
                    Year = year,
                    Term = $"{year - 1} to {year}",
                    Total = item.Total,
                    Mainstream = item.Mainstream,
                    Resourced = item.Resourced,
                    Special = item.Special,
                    Independent = item.Independent,
                    Hospital = item.Hospital,
                    Post16 = item.Post16,
                    Other = item.Other
                };
            }
        }
    }
}