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

public interface IIncomeService
{
    Task<IncomeModelDto?> GetAsync(string urn, CancellationToken cancellationToken = default);
    Task<(YearsModelDto? years, IEnumerable<IncomeHistoryModelDto> rows)> GetHistoryAsync(string urn, string dimension, CancellationToken cancellationToken = default);
    Task<(YearsModelDto? years, IEnumerable<IncomeHistoryModelDto> rows)> GetComparatorAveHistoryAsync(string urn, string dimension, CancellationToken cancellationToken = default);
    Task<(YearsModelDto?, IEnumerable<IncomeHistoryModelDto>)> GetNationalAvgHistoryAsync(string overallPhase, string financeType, string dimension, CancellationToken cancellationToken = default);
}

[ExcludeFromCodeCoverage]
public class IncomeService(IDatabaseFactory dbFactory, ICacheKeyFactory cacheKeyFactory, IDistributedCache cache) : IIncomeService
{
    public async Task<IncomeModelDto?> GetAsync(string urn, CancellationToken cancellationToken = default)
    {
        var builder = new IncomeSchoolDefaultCurrentQuery(Dimensions.Finance.Actuals)
            .WhereUrnEqual(urn);

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<IncomeModelDto>(builder, cancellationToken);
    }

    public async Task<(YearsModelDto?, IEnumerable<IncomeHistoryModelDto>)> GetHistoryAsync(string urn, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await QueryYearsSchoolAsync(conn, urn, cancellationToken);

        if (years == null)
        {
            return (null, []);
        }

        var historyBuilder = new IncomeSchoolDefaultQuery(dimension)
            .WhereUrnEqual(urn)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<IncomeHistoryModelDto>(historyBuilder, cancellationToken));
    }

    public async Task<(YearsModelDto?, IEnumerable<IncomeHistoryModelDto>)> GetComparatorAveHistoryAsync(string urn, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await QueryYearsSchoolAsync(conn, urn, cancellationToken);

        if (years == null)
        {
            return (null, []);
        }

        var historyBuilder = new IncomeSchoolDefaultComparatorAvgQuery(dimension)
            .WhereUrnEqual(urn)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<IncomeHistoryModelDto>(historyBuilder, cancellationToken));
    }

    public async Task<(YearsModelDto?, IEnumerable<IncomeHistoryModelDto>)> GetNationalAvgHistoryAsync(string overallPhase, string financeType, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await QueryYearsOverallPhaseAsync(conn, overallPhase, financeType, cancellationToken);

        if (years == null)
        {
            return (null, []);
        }

        var cacheKey = cacheKeyFactory.CreateIncomeHistoryNationalAverageCacheKey(years.EndYear, overallPhase, financeType, dimension);
        var history = await cache.GetSetAsync(cacheKey, () => GetNationalAvgHistoryAsync(conn, years, overallPhase, financeType, dimension, cancellationToken));
        return (years, history);
    }

    private static async Task<YearsModelDto?> QueryYearsOverallPhaseAsync(IDatabaseConnection conn, string overallPhase, string financeType, CancellationToken cancellationToken = default)
    {
        var builder = new YearsOverallPhaseQuery(overallPhase, financeType);
        return await conn.QueryFirstOrDefaultAsync<YearsModelDto>(builder, cancellationToken);
    }

    private static async Task<IEnumerable<IncomeHistoryModelDto>> GetNationalAvgHistoryAsync(IDatabaseConnection conn, YearsModelDto years, string overallPhase, string financeType, string dimension, CancellationToken cancellationToken = default)
    {
        var historyBuilder = new IncomeSchoolDefaultNationalAveQuery(dimension)
            .WhereOverallPhaseEqual(overallPhase)
            .WhereFinanceTypeEqual(financeType)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return await conn.QueryAsync<IncomeHistoryModelDto>(historyBuilder, cancellationToken);
    }

    private static async Task<YearsModelDto?> QueryYearsSchoolAsync(IDatabaseConnection conn, string urn, CancellationToken cancellationToken = default)
    {
        var builder = new YearsSchoolQuery(urn);
        return await conn.QueryFirstOrDefaultAsync<YearsModelDto>(builder, cancellationToken);
    }
}