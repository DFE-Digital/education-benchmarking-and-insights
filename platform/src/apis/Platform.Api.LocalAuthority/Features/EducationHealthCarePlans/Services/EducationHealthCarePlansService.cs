using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Models;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Services;

public interface IEducationHealthCarePlansService
{
    Task<IEnumerable<EducationHealthCarePlansDto>> QueryAsync(string[] codes, string dimension, CancellationToken cancellationToken = default);
    Task<(YearsModelDto?, IEnumerable<EducationHealthCarePlansYearDto>)> QueryHistoryAsync(string[] codes, string dimension, CancellationToken cancellationToken = default);
}

public class EducationHealthCarePlansService(IDatabaseFactory dbFactory) : IEducationHealthCarePlansService
{
    public async Task<IEnumerable<EducationHealthCarePlansDto>> QueryAsync(string[] codes, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var builder = new LocalAuthorityEducationHealthCarePlansDefaultCurrentQuery(dimension)
            .WhereLaCodesIn(codes);

        return await conn.QueryAsync<EducationHealthCarePlansDto>(builder, cancellationToken);
    }

    public async Task<(YearsModelDto?, IEnumerable<EducationHealthCarePlansYearDto>)> QueryHistoryAsync(string[] codes, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await QueryYearsLocalAuthorityAsync(conn, codes.First(), cancellationToken);
        if (years == null)
        {
            return (null, []);
        }

        var builder = new LocalAuthorityEducationHealthCarePlansDefaultQuery(dimension)
            .WhereLaCodesIn(codes)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        var results = await conn.QueryAsync<EducationHealthCarePlansYearDto>(builder, cancellationToken);
        return (years, results);
    }

    private static async Task<YearsModelDto?> QueryYearsLocalAuthorityAsync(IDatabaseConnection conn, string code, CancellationToken cancellationToken = default)
    {
        var builder = new YearsLocalAuthorityQuery(code);
        return await conn.QueryFirstOrDefaultAsync<YearsModelDto>(builder, cancellationToken);
    }
}