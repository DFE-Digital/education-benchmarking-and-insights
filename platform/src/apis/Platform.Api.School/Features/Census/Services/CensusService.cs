using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.School.Features.Census.Models;
using Platform.Cache;
using Platform.Domain;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.School.Features.Census.Services;

public interface ICensusService
{
    Task<CensusModelDto?> GetAsync(string urn, CancellationToken cancellationToken = default);
    Task<(YearsModelDto?, IEnumerable<CensusHistoryModelDto>)> GetHistoryAsync(string urn, string dimension, CancellationToken cancellationToken = default);
    Task<CensusModelDto?> GetUserDefinedAsync(string urn, string identifier, string dimension, CancellationToken cancellationToken = default);
    Task<IEnumerable<CensusModelDto>> QueryAsync(string[] urns, string? companyNumber, string? laCode, string? phase, string dimension, CancellationToken cancellationToken = default);
    Task<(YearsModelDto?, IEnumerable<CensusHistoryModelDto>)> GetComparatorAveHistoryAsync(string urn, string dimension, CancellationToken cancellationToken = default);
    Task<(YearsModelDto?, IEnumerable<CensusHistoryModelDto>)> GetNationalAvgHistoryAsync(string overallPhase, string financeType, string dimension, CancellationToken cancellationToken = default);
    Task<IEnumerable<SeniorLeadershipResponse>> QuerySeniorLeadershipAsync(string[] urns, string dimension, CancellationToken cancellationToken = default);
}

[ExcludeFromCodeCoverage]
public class CensusService(IDatabaseFactory dbFactory, ICacheKeyFactory cacheKeyFactory, IDistributedCache cache) : ICensusService
{
    public async Task<CensusModelDto?> GetAsync(string urn, CancellationToken cancellationToken = default)
    {
        var builder = new CensusSchoolDefaultCurrentQuery(Dimensions.Census.Total)
            .WhereUrnEqual(urn);

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<CensusModelDto>(builder, cancellationToken);
    }

    public async Task<(YearsModelDto?, IEnumerable<CensusHistoryModelDto>)> GetHistoryAsync(string urn, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await QueryYearsSchoolAsync(conn, urn, cancellationToken);

        if (years == null)
        {
            return (null, []);
        }

        var historyBuilder = new CensusSchoolDefaultQuery(dimension)
            .WhereUrnEqual(urn)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<CensusHistoryModelDto>(historyBuilder, cancellationToken));
    }

    public async Task<CensusModelDto?> GetUserDefinedAsync(string urn, string identifier, string dimension, CancellationToken cancellationToken = default)
    {
        var builder = new CensusSchoolCustomQuery(dimension)
            .WhereUrnEqual(urn)
            .WhereRunIdEqual(identifier);

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<CensusModelDto>(builder, cancellationToken);
    }

    public async Task<IEnumerable<CensusModelDto>> QueryAsync(string[] urns, string? companyNumber, string? laCode, string? phase, string dimension, CancellationToken cancellationToken = default)
    {
        var builder = new CensusSchoolDefaultCurrentQuery(dimension);
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
        return await conn.QueryAsync<CensusModelDto>(builder, cancellationToken);
    }

    public async Task<(YearsModelDto?, IEnumerable<CensusHistoryModelDto>)> GetComparatorAveHistoryAsync(string urn, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await QueryYearsSchoolAsync(conn, urn, cancellationToken);

        if (years == null)
        {
            return (null, []);
        }

        var historyBuilder = new CensusSchoolDefaultComparatorAveQuery(dimension)
            .WhereUrnEqual(urn)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<CensusHistoryModelDto>(historyBuilder, cancellationToken));
    }

    public async Task<(YearsModelDto?, IEnumerable<CensusHistoryModelDto>)> GetNationalAvgHistoryAsync(string overallPhase, string financeType, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await QueryYearsOverallPhaseAsync(conn, overallPhase, financeType, cancellationToken);

        if (years == null)
        {
            return (null, []);
        }

        var cacheKey = cacheKeyFactory.CreateCensusHistoryNationalAverageCacheKey(years.EndYear, overallPhase, financeType, dimension);
        var history = await cache.GetSetAsync(cacheKey, () => GetNationalAvgHistoryAsync(conn, years, overallPhase, financeType, dimension, cancellationToken));
        return (years, history);
    }

    public async Task<IEnumerable<SeniorLeadershipResponse>> QuerySeniorLeadershipAsync(string[] urns, string dimension, CancellationToken cancellationToken = default)
    {
        var builder = new SchoolSeniorLeadershipDefaultCurrentQuery(dimension);

        if (urns.Length != 0)
        {
            builder.WhereUrnIn(urns);
        }
        else
        {
            throw new ArgumentNullException(nameof(urns), $"{nameof(urns)} must be supplied");
        }

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<SeniorLeadershipResponse>(builder, cancellationToken);
    }

    private static async Task<IEnumerable<CensusHistoryModelDto>> GetNationalAvgHistoryAsync(IDatabaseConnection conn, YearsModelDto years, string overallPhase, string financeType, string dimension, CancellationToken cancellationToken = default)
    {
        var historyBuilder = new CensusSchoolDefaultNationalAveQuery(dimension)
            .WhereOverallPhaseEqual(overallPhase)
            .WhereFinanceTypeEqual(financeType)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return await conn.QueryAsync<CensusHistoryModelDto>(historyBuilder, cancellationToken);
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