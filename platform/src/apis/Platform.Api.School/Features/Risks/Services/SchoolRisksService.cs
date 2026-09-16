using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.School.Features.Risks.Models;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.School.Features.Risks.Services;

public interface ISchoolRisksService
{
    Task<(YearsModelDto?, IEnumerable<RisksHistoryModelDto>)> GetHistoryAsync(string urn, CancellationToken cancellationToken = default);
}

public class SchoolRisksService(IDatabaseFactory dbFactory) : ISchoolRisksService
{
    public async Task<(YearsModelDto?, IEnumerable<RisksHistoryModelDto>)> GetHistoryAsync(string urn, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();

        var years = await QueryYearsSchoolAsync(conn, urn, cancellationToken);

        if (years == null)
        {
            return (null, []);
        }

        var historyBuilder = new SchoolRisksDefaultQuery()
            .WhereUrnEqual(urn)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<RisksHistoryModelDto>(historyBuilder, cancellationToken));
    }

    private static async Task<YearsModelDto?> QueryYearsSchoolAsync(IDatabaseConnection conn, string urn, CancellationToken cancellationToken = default)
    {
        var builder = new YearsSchoolQuery(urn);
        return await conn.QueryFirstOrDefaultAsync<YearsModelDto>(builder, cancellationToken);
    }
}
