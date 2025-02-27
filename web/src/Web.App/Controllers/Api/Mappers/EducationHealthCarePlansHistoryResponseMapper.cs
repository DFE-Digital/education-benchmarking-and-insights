using Web.App.Controllers.Api.Responses;
using Web.App.Domain.NonFinancial;

namespace Web.App.Controllers.Api.Mappers;

public static class EducationHealthCarePlansHistoryResponseResponseMapper
{
    public static IEnumerable<EducationHealthCarePlansHistoryResponse> MapToApiResponse(this History<LocalAuthorityNumberOfPlansYear>? history, string code)
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
                continue;
            }

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