using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Models;

namespace Platform.Api.NonFinancial.Features.EducationHealthCarePlans;

public static class Mapper
{
    public static LocalAuthorityNumberOfPlansResponse MapToLocalAuthorityNumberOfPlansResponse(LocalAuthorityNumberOfPlans value)
    {
        return new LocalAuthorityNumberOfPlansResponse
        {
            Code = value.LaCode,
            Name = value.Name,
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

    public static LocalAuthorityNumberOfPlansYearResponse MapToLocalAuthorityNumberOfPlansYearResponse(LocalAuthorityNumberOfPlansYear value)
    {
        int? year = null;
        if (int.TryParse(value.RunId, out var parsed))
        {
            year = parsed;
        }

        return new LocalAuthorityNumberOfPlansYearResponse
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