using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.School.Features.Accounts.Models;
using Platform.Cache;
using Platform.Domain;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.School.Features.Accounts.Services;

public interface IBalanceService
{
    Task<BalanceModelDto?> GetAsync(string urn, CancellationToken cancellationToken = default);
    Task<(YearsModelDto? years, IEnumerable<BalanceHistoryModelDto> rows)> GetHistoryAsync(string urn, string dimension, CancellationToken cancellationToken = default);
    Task<(YearsModelDto?, IEnumerable<BalanceHistoryModelDto>)> GetNationalAvgHistoryAsync(string overallPhase, string financeType, string dimension, CancellationToken cancellationToken = default);
}

[ExcludeFromCodeCoverage]
public class BalanceService(IDatabaseFactory dbFactory, ICacheKeyFactory cacheKeyFactory, IDistributedCache cache) : IBalanceService
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

    public async Task<(YearsModelDto?, IEnumerable<BalanceHistoryModelDto>)> GetNationalAvgHistoryAsync(string overallPhase, string financeType, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await QueryYearsOverallPhaseAsync(conn, overallPhase, financeType, cancellationToken);

        if (years == null)
        {
            return (null, []);
        }

        var cacheKey = cacheKeyFactory.CreateBalanceHistoryNationalAverageCacheKey(years.EndYear, overallPhase, financeType, dimension);
        var history = await cache.GetSetAsync(cacheKey, () => GetNationalAvgHistoryAsync(conn, years, overallPhase, financeType, dimension, cancellationToken));
        return (years, history);
    }

    private static async Task<YearsModelDto?> QueryYearsOverallPhaseAsync(IDatabaseConnection conn, string overallPhase, string financeType, CancellationToken cancellationToken = default)
    {
        var builder = new YearsOverallPhaseQuery(overallPhase, financeType);
        return await conn.QueryFirstOrDefaultAsync<YearsModelDto>(builder, cancellationToken);
    }

    private static async Task<IEnumerable<BalanceHistoryModelDto>> GetNationalAvgHistoryAsync(IDatabaseConnection conn, YearsModelDto years, string overallPhase, string financeType, string dimension, CancellationToken cancellationToken = default)
    {
        var historyBuilder = new BalanceSchoolDefaultNationalAveQuery(dimension)
            .WhereOverallPhaseEqual(overallPhase)
            .WhereFinanceTypeEqual(financeType)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return await conn.QueryAsync<BalanceHistoryModelDto>(historyBuilder, cancellationToken);
    }
    private static async Task<YearsModelDto?> QueryYearsSchoolAsync(IDatabaseConnection conn, string urn, CancellationToken cancellationToken = default)
    {
        var builder = new YearsSchoolQuery(urn);
        return await conn.QueryFirstOrDefaultAsync<YearsModelDto>(builder, cancellationToken);
    }
}