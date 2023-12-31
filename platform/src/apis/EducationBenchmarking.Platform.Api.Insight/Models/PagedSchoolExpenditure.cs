using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EducationBenchmarking.Platform.Shared;

namespace EducationBenchmarking.Platform.Api.Insight.Models;

[ExcludeFromCodeCoverage]
public class PagedSchoolExpenditure : PagedResults<SchoolExpenditure>
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