using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EducationBenchmarking.Platform.Domain.DataObjects;

namespace EducationBenchmarking.Platform.Domain.Responses;

[ExcludeFromCodeCoverage]
public record PagedSchoolExpenditure : PagedResults<SchoolExpenditure>
{
    public static PagedSchoolExpenditure Create(IEnumerable<SchoolTrustFinancialDataObject> results, int page,
        int pageSize)
    {
        var schools = results.Select(SchoolExpenditure.Create).ToList();

        return new PagedSchoolExpenditure
        {
            Page = page,
            PageSize = pageSize,
            Results = schools,
            TotalResults = schools.Count
        };
    }
}