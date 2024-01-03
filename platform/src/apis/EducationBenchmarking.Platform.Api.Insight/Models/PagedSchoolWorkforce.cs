using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EducationBenchmarking.Platform.Infrastructure.Cosmos;
using EducationBenchmarking.Platform.Shared;

namespace EducationBenchmarking.Platform.Api.Insight.Models;

[ExcludeFromCodeCoverage]
public class PagedSchoolWorkforce : PagedResults<SchoolWorkforce>
{
    public static PagedSchoolWorkforce Create(IEnumerable<SchoolTrustFinancialDataObject> results, int page,
        int pageSize)
    {
        var schools = new List<SchoolWorkforce>();

        foreach (var result in results)
        {
            schools.Add(new SchoolWorkforce
            {
                Urn = result.URN.ToString(),
                Name = result.SchoolName
            });
        }

        return new PagedSchoolWorkforce
        {
            Page = page,
            PageSize = pageSize,
            Results = schools,
            TotalResults = schools.Count
        };
    }
}