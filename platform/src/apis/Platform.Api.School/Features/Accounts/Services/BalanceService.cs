using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.School.Features.Accounts.Models;
using Platform.Domain;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.School.Features.Accounts.Services;

public interface IBalanceService
{
    Task<BalanceModelDto?> GetAsync(string urn, CancellationToken cancellationToken = default);
    Task<(YearsModelDto? years, IEnumerable<BalanceHistoryModelDto> rows)> GetHistoryAsync(string urn, string dimension, CancellationToken cancellationToken = default);
}

[ExcludeFromCodeCoverage]
public class BalanceService(IDatabaseFactory dbFactory) : IBalanceService
{
    public async Task<BalanceModelDto?> GetAsync(string urn, CancellationToken cancellationToken = default)
    {
        var builder = new BalanceSchoolDefaultCurrentQuery(Dimensions.Finance.Actuals)
            .WhereUrnEqual(urn);

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<BalanceModelDto>(builder, cancellationToken);
    }

    public async Task<(YearsModelDto?, IEnumerable<BalanceHistoryModelDto>)> GetHistoryAsync(string urn, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await QueryYearsSchoolAsync(conn, urn, cancellationToken);

        if (years == null)
        {
            return (null, []);
        }

        var historyBuilder = new BalanceSchoolDefaultQuery(dimension)
            .WhereUrnEqual(urn)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<BalanceHistoryModelDto>(historyBuilder, cancellationToken));
    }

    private static async Task<YearsModelDto?> QueryYearsSchoolAsync(IDatabaseConnection conn, string urn, CancellationToken cancellationToken = default)
    {
        var builder = new YearsSchoolQuery(urn);
        return await conn.QueryFirstOrDefaultAsync<YearsModelDto>(builder, cancellationToken);
    }
}