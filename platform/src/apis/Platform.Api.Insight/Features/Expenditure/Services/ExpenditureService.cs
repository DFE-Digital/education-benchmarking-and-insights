using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.Insight.Features.Expenditure.Models;
using Platform.Api.Insight.Shared;
using Platform.Cache;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Insight.Features.Expenditure.Services;

public interface IExpenditureService
{
    Task<ExpenditureSchoolModel?> GetSchoolAsync(string urn, string dimension);
    Task<(YearsModel?, IEnumerable<ExpenditureHistoryModel>)> GetSchoolHistoryAsync(string urn, string dimension, CancellationToken cancellationToken = default);
    Task<IEnumerable<ExpenditureSchoolModel>> QuerySchoolsAsync(string[] urns, string? companyNumber, string? laCode, string? phase, string dimension);
    Task<ExpenditureSchoolModel?> GetCustomSchoolAsync(string urn, string identifier, string dimension);
    Task<ExpenditureTrustModel?> GetTrustAsync(string companyNumber, string dimension);
    Task<(YearsModel?, IEnumerable<ExpenditureHistoryModel>)> GetTrustHistoryAsync(string companyNumber, string dimension);
    Task<IEnumerable<ExpenditureTrustModel>> QueryTrustsAsync(string[] companyNumbers, string dimension);
    Task<(YearsModel?, IEnumerable<ExpenditureHistoryModel>)> GetComparatorAveHistoryAsync(string urn, string dimension, CancellationToken cancellationToken = default);
    Task<(YearsModel?, IEnumerable<ExpenditureHistoryModel>)> GetNationalAvgHistoryAsync(string overallPhase, string financeType, string dimension, CancellationToken cancellationToken = default);
}

[ExcludeFromCodeCoverage]
public class ExpenditureService(IDatabaseFactory dbFactory, ICacheKeyFactory cacheKeyFactory, IDistributedCache cache) : IExpenditureService
{
    public async Task<ExpenditureSchoolModel?> GetSchoolAsync(string urn, string dimension)
    {
        using var conn = await dbFactory.GetConnection();
        var builder = new ExpenditureSchoolDefaultCurrentQuery(dimension)
            .WhereUrnEqual(urn);

        return await conn.QueryFirstOrDefaultAsync<ExpenditureSchoolModel>(builder);
    }

    public async Task<(YearsModel?, IEnumerable<ExpenditureHistoryModel>)> GetSchoolHistoryAsync(string urn, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await conn.QueryYearsSchoolAsync(urn, cancellationToken);

        if (years == null)
        {
            return (null, Array.Empty<ExpenditureHistoryModel>());
        }

        var historyBuilder = new ExpenditureSchoolDefaultQuery(dimension)
            .WhereUrnEqual(urn)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<ExpenditureHistoryModel>(historyBuilder, cancellationToken));
    }

    public async Task<IEnumerable<ExpenditureSchoolModel>> QuerySchoolsAsync(string[] urns, string? companyNumber, string? laCode, string? phase, string dimension)
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
        return await conn.QueryAsync<ExpenditureSchoolModel>(builder);
    }

    public async Task<ExpenditureSchoolModel?> GetCustomSchoolAsync(string urn, string identifier, string dimension)
    {
        using var conn = await dbFactory.GetConnection();
        var builder = new ExpenditureSchoolCustomQuery(dimension)
            .WhereUrnEqual(urn);

        return await conn.QueryFirstOrDefaultAsync<ExpenditureSchoolModel>(builder);
    }

    public async Task<ExpenditureTrustModel?> GetTrustAsync(string companyNumber, string dimension)
    {
        using var conn = await dbFactory.GetConnection();
        var builder = new ExpenditureTrustDefaultCurrentQuery(dimension)
            .WhereCompanyNumberEqual(companyNumber);

        return await conn.QueryFirstOrDefaultAsync<ExpenditureTrustModel>(builder);
    }

    public async Task<(YearsModel?, IEnumerable<ExpenditureHistoryModel>)> GetTrustHistoryAsync(string companyNumber, string dimension)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await conn.QueryYearsTrustAsync(companyNumber);

        if (years == null)
        {
            return (null, Array.Empty<ExpenditureHistoryModel>());
        }

        var historyBuilder = new ExpenditureTrustDefaultQuery(dimension)
            .WhereCompanyNumberEqual(companyNumber)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<ExpenditureHistoryModel>(historyBuilder));
    }

    public async Task<IEnumerable<ExpenditureTrustModel>> QueryTrustsAsync(string[] companyNumbers, string dimension)
    {
        using var conn = await dbFactory.GetConnection();
        var builder = new ExpenditureTrustDefaultCurrentQuery(dimension)
            .WhereCompanyNumberIn(companyNumbers);

        return await conn.QueryAsync<ExpenditureTrustModel>(builder);
    }

    public async Task<(YearsModel?, IEnumerable<ExpenditureHistoryModel>)> GetComparatorAveHistoryAsync(string urn, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await conn.QueryYearsSchoolAsync(urn, cancellationToken);

        if (years == null)
        {
            return (null, Array.Empty<ExpenditureHistoryModel>());
        }

        var historyBuilder = new ExpenditureSchoolDefaultComparatorAvgQuery(dimension)
            .WhereUrnEqual(urn)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<ExpenditureHistoryModel>(historyBuilder, cancellationToken));
    }

    public async Task<(YearsModel?, IEnumerable<ExpenditureHistoryModel>)> GetNationalAvgHistoryAsync(string overallPhase, string financeType, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await conn.QueryYearsOverallPhaseAsync(overallPhase, financeType, cancellationToken);

        if (years == null)
        {
            return (null, Array.Empty<ExpenditureHistoryModel>());
        }

        var cacheKey = cacheKeyFactory.CreateExpenditureHistoryNationalAverageCacheKey(years.EndYear, overallPhase, financeType, dimension);
        var history = await cache.GetSetAsync(cacheKey, () => GetNationalAvgHistoryAsync(conn, years, overallPhase, financeType, dimension, cancellationToken));
        return (years, history);
    }

    private static async Task<IEnumerable<ExpenditureHistoryModel>> GetNationalAvgHistoryAsync(IDatabaseConnection conn, YearsModel years, string overallPhase, string financeType, string dimension, CancellationToken cancellationToken)
    {
        var historyBuilder = new ExpenditureSchoolDefaultNationalAveQuery(dimension)
            .WhereOverallPhaseEqual(overallPhase)
            .WhereFinanceTypeEqual(financeType)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return await conn.QueryAsync<ExpenditureHistoryModel>(historyBuilder, cancellationToken);
    }
}