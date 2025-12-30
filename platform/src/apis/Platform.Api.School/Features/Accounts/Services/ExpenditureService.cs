using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.School.Features.Accounts.Models;
using Platform.Cache;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.School.Features.Accounts.Services;

public interface IExpenditureService
{
    Task<ExpenditureModelDto?> GetAsync(string urn, string dimension, CancellationToken cancellationToken = default);
    Task<(YearsModelDto?, IEnumerable<ExpenditureHistoryModelDto>)> GetHistoryAsync(string urn, string dimension, CancellationToken cancellationToken = default);
    Task<IEnumerable<ExpenditureModelDto>> QueryAsync(string[] urns, string? companyNumber, string? laCode, string? phase, string dimension, CancellationToken cancellationToken = default);
    Task<ExpenditureModelDto?> GetUserDefinedAsync(string urn, string identifier, string dimension, CancellationToken cancellationToken = default);
    Task<(YearsModelDto?, IEnumerable<ExpenditureHistoryModelDto>)> GetComparatorAveHistoryAsync(string urn, string dimension, CancellationToken cancellationToken = default);
    Task<(YearsModelDto?, IEnumerable<ExpenditureHistoryModelDto>)> GetNationalAvgHistoryAsync(string overallPhase, string financeType, string dimension, CancellationToken cancellationToken = default);
}

[ExcludeFromCodeCoverage]
public class ExpenditureService(IDatabaseFactory dbFactory, ICacheKeyFactory cacheKeyFactory, IDistributedCache cache) : IExpenditureService
{
    public async Task<ExpenditureModelDto?> GetAsync(string urn, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var builder = new ExpenditureSchoolDefaultCurrentQuery(dimension)
            .WhereUrnEqual(urn);

        return await conn.QueryFirstOrDefaultAsync<ExpenditureModelDto>(builder, cancellationToken);
    }

    public async Task<(YearsModelDto?, IEnumerable<ExpenditureHistoryModelDto>)> GetHistoryAsync(string urn, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await QueryYearsSchoolAsync(conn, urn, cancellationToken);

        if (years == null)
        {
            return (null, []);
        }

        var historyBuilder = new ExpenditureSchoolDefaultQuery(dimension)
            .WhereUrnEqual(urn)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<ExpenditureHistoryModelDto>(historyBuilder, cancellationToken));
    }

    public async Task<IEnumerable<ExpenditureModelDto>> QueryAsync(string[] urns, string? companyNumber, string? laCode, string? phase, string dimension, CancellationToken cancellationToken = default)
    {
        var builder = new ExpenditureSchoolDefaultCurrentQuery(dimension);

        if (urns.Length != 0)
        {
            builder.WhereUrnIn(urns);
        }
        else if (!string.IsNullOrWhiteSpace(companyNumber))
        {
            builder
                .WhereTrustCompanyNumberEqual(companyNumber)
                .WhereOverallPhaseEqual(phase);
        }
        else if (!string.IsNullOrWhiteSpace(laCode))
        {
            builder
                .WhereLaCodeEqual(laCode)
                .WhereOverallPhaseEqual(phase);
        }
        else
        {
            throw new ArgumentNullException(nameof(urns), $"{nameof(urns)} or {nameof(companyNumber)} or {nameof(laCode)} must be supplied");
        }

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<ExpenditureModelDto>(builder, cancellationToken);
    }

    public async Task<ExpenditureModelDto?> GetUserDefinedAsync(string urn, string identifier, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var builder = new ExpenditureSchoolCustomQuery(dimension)
            .WhereUrnEqual(urn)
            .WhereRunIdEqual(identifier);

        return await conn.QueryFirstOrDefaultAsync<ExpenditureModelDto>(builder, cancellationToken);
    }

    public async Task<(YearsModelDto?, IEnumerable<ExpenditureHistoryModelDto>)> GetComparatorAveHistoryAsync(string urn, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await QueryYearsSchoolAsync(conn, urn, cancellationToken);

        if (years == null)
        {
            return (null, []);
        }

        var historyBuilder = new ExpenditureSchoolDefaultComparatorAvgQuery(dimension)
            .WhereUrnEqual(urn)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<ExpenditureHistoryModelDto>(historyBuilder, cancellationToken));
    }

    public async Task<(YearsModelDto?, IEnumerable<ExpenditureHistoryModelDto>)> GetNationalAvgHistoryAsync(string overallPhase, string financeType, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await QueryYearsOverallPhaseAsync(conn, overallPhase, financeType, cancellationToken);

        if (years == null)
        {
            return (null, []);
        }

        var cacheKey = cacheKeyFactory.CreateExpenditureHistoryNationalAverageCacheKey(years.EndYear, overallPhase, financeType, dimension);
        var history = await cache.GetSetAsync(cacheKey, () => GetNationalAvgHistoryAsync(conn, years, overallPhase, financeType, dimension, cancellationToken));
        return (years, history);
    }

    private static async Task<IEnumerable<ExpenditureHistoryModelDto>> GetNationalAvgHistoryAsync(IDatabaseConnection conn, YearsModelDto years, string overallPhase, string financeType, string dimension, CancellationToken cancellationToken = default)
    {
        var historyBuilder = new ExpenditureSchoolDefaultNationalAveQuery(dimension)
            .WhereOverallPhaseEqual(overallPhase)
            .WhereFinanceTypeEqual(financeType)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return await conn.QueryAsync<ExpenditureHistoryModelDto>(historyBuilder, cancellationToken);
    }

    private static async Task<YearsModelDto?> QueryYearsOverallPhaseAsync(IDatabaseConnection conn, string overallPhase, string financeType, CancellationToken cancellationToken = default)
    {
        var builder = new YearsOverallPhaseQuery(overallPhase, financeType);
        return await conn.QueryFirstOrDefaultAsync<YearsModelDto>(builder, cancellationToken);
    }

    private static async Task<YearsModelDto?> QueryYearsSchoolAsync(IDatabaseConnection conn, string urn, CancellationToken cancellationToken = default)
    {
        var builder = new YearsSchoolQuery(urn);
        return await conn.QueryFirstOrDefaultAsync<YearsModelDto>(builder, cancellationToken);
    }
}