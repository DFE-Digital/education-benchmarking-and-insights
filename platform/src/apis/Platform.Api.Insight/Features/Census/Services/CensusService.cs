using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.Insight.Features.Census.Models;
using Platform.Api.Insight.Shared;
using Platform.Cache;
using Platform.Domain;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Insight.Features.Census.Services;

public interface ICensusService
{
    Task<CensusSchoolModel?> GetAsync(string urn, string dimension = Dimensions.Census.Total);
    Task<(YearsModel?, IEnumerable<CensusHistoryModel>)> GetSchoolHistoryAsync(string urn, string dimension = Dimensions.Census.Total, CancellationToken cancellationToken = default);
    Task<CensusSchoolModel?> GetCustomAsync(string urn, string identifier, string dimension = Dimensions.Census.Total);
    Task<IEnumerable<CensusSchoolModel>> QueryAsync(string[] urns, string? companyNumber, string? laCode, string? phase, string dimension = Dimensions.Census.Total);
    Task<(YearsModel?, IEnumerable<CensusHistoryModel>)> GetComparatorAveHistoryAsync(string urn, string dimension = Dimensions.Census.Total, CancellationToken cancellationToken = default);
    Task<(YearsModel?, IEnumerable<CensusHistoryModel>)> GetNationalAvgHistoryAsync(string overallPhase, string financeType, string dimension = Dimensions.Census.Total, CancellationToken cancellationToken = default);
}

[ExcludeFromCodeCoverage]
public class CensusService(IDatabaseFactory dbFactory, ICacheKeyFactory cacheKeyFactory, IDistributedCache cache) : ICensusService
{
    public async Task<CensusSchoolModel?> GetAsync(string urn, string dimension = Dimensions.Census.Total)
    {
        var builder = new CensusSchoolDefaultCurrentQuery(dimension)
            .WhereUrnEqual(urn);

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<CensusSchoolModel>(builder);
    }

    public async Task<(YearsModel?, IEnumerable<CensusHistoryModel>)> GetSchoolHistoryAsync(string urn, string dimension = Dimensions.Census.Total, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await conn.QueryYearsSchoolAsync(urn, cancellationToken);

        if (years == null)
        {
            return (null, Array.Empty<CensusHistoryModel>());
        }

        var historyBuilder = new CensusSchoolDefaultQuery(dimension)
            .WhereUrnEqual(urn)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<CensusHistoryModel>(historyBuilder, cancellationToken));
    }

    public async Task<CensusSchoolModel?> GetCustomAsync(string urn, string identifier, string dimension = Dimensions.Census.Total)
    {
        var builder = new CensusSchoolCustomQuery(dimension)
            .WhereUrnEqual(urn)
            .WhereRunIdEqual(identifier);

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<CensusSchoolModel>(builder);
    }

    public async Task<IEnumerable<CensusSchoolModel>> QueryAsync(string[] urns, string? companyNumber, string? laCode, string? phase, string dimension = Dimensions.Census.Total)
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
        return await conn.QueryAsync<CensusSchoolModel>(builder);
    }

    public async Task<(YearsModel?, IEnumerable<CensusHistoryModel>)> GetComparatorAveHistoryAsync(string urn, string dimension = Dimensions.Census.Total, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await conn.QueryYearsSchoolAsync(urn, cancellationToken);

        if (years == null)
        {
            return (null, Array.Empty<CensusHistoryModel>());
        }

        var historyBuilder = new CensusSchoolDefaultComparatorAveQuery(dimension)
            .WhereUrnEqual(urn)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<CensusHistoryModel>(historyBuilder, cancellationToken));
    }

    public async Task<(YearsModel?, IEnumerable<CensusHistoryModel>)> GetNationalAvgHistoryAsync(string overallPhase, string financeType, string dimension = Dimensions.Census.Total, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await conn.QueryYearsOverallPhaseAsync(overallPhase, financeType, cancellationToken);

        if (years == null)
        {
            return (null, Array.Empty<CensusHistoryModel>());
        }

        var cacheKey = cacheKeyFactory.CreateCensusHistoryNationalAverageCacheKey(years.EndYear, overallPhase, financeType, dimension);
        var history = await cache.GetSetAsync(cacheKey, () => GetNationalAvgHistoryAsync(conn, years, overallPhase, financeType, dimension, cancellationToken));
        return (years, history);
    }

    private static async Task<IEnumerable<CensusHistoryModel>> GetNationalAvgHistoryAsync(IDatabaseConnection conn, YearsModel years, string overallPhase, string financeType, string dimension, CancellationToken cancellationToken)
    {
        var historyBuilder = new CensusSchoolDefaultNationalAveQuery(dimension)
            .WhereOverallPhaseEqual(overallPhase)
            .WhereFinanceTypeEqual(financeType)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return await conn.QueryAsync<CensusHistoryModel>(historyBuilder, cancellationToken);
    }
}