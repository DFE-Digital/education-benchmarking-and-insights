using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Platform.Domain.DataObjects;

namespace Platform.Domain.Responses;

[ExcludeFromCodeCoverage]
public record PagedSchoolWorkforce : PagedResults<SchoolWorkforce>
{
    public static PagedSchoolWorkforce Create(IEnumerable<SchoolTrustFinancialDataObject> results, int page,
        int pageSize)
    {

        var schools = results.Select(SchoolWorkforce.Create).ToList();
        return new PagedSchoolWorkforce
        {
            Page = page,
            PageSize = pageSize,
            Results = schools,
            TotalResults = schools.Count
        };
    }
}