using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Platform.Api.School.Features.Risks.Models;

namespace Platform.Api.School.Features.Risks;

[ExcludeFromCodeCoverage]
public static class Mapper
{
    public static SchoolRisksHistoryResponse MapToApiResponse(this YearsModelDto years, IEnumerable<RisksHistoryModelDto> models)
    {
        return new SchoolRisksHistoryResponse
        {
            StartYear = years.StartYear,
            EndYear = years.EndYear,
            Rows = models.Select(x => x.MapToApiResponse())
        };
    }

    private static SchoolRisksHistoryRowResponse MapToApiResponse(this RisksHistoryModelDto model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model), "Model cannot be null.");
        }

        return new SchoolRisksHistoryRowResponse
        {
            Year = model.RunId,
            Urn = model.Urn,
            SchoolName = model.SchoolName,
            OverallGrade = model.OverallGrade,
            Overall = model.Overall,
            OverallMax = model.OverallMax,
            Financial = model.Financial,
            FinancialMax = model.FinancialMax,
            SchoolAndPupil = model.SchoolAndPupil,
            SchoolAndPupilMax = model.SchoolAndPupilMax,
            EducationalPerformance = model.EducationalPerformance,
            EducationalPerformanceMax = model.EducationalPerformanceMax,
        };
    }
}
