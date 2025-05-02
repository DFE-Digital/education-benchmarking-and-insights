using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.Insight.Features.Balance.Models;
using Platform.Api.Insight.Shared;
using Platform.Domain;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Insight.Features.Balance.Services;

public interface IBalanceService
{
    Task<BalanceSchoolModel?> GetSchoolAsync(string urn, CancellationToken cancellationToken = default);
    Task<(YearsModel? years, IEnumerable<BalanceHistoryModel> rows)> GetSchoolHistoryAsync(string urn, string dimension, CancellationToken cancellationToken = default);
    Task<BalanceTrustModel?> GetTrustAsync(string companyNumber, CancellationToken cancellationToken = default);
    Task<(YearsModel?, IEnumerable<BalanceHistoryModel>)> GetTrustHistoryAsync(string companyNumber, string dimension, CancellationToken cancellationToken = default);
    Task<IEnumerable<BalanceTrustModel>> QueryTrustsAsync(string[] companyNumbers, string dimension, CancellationToken cancellationToken = default);
}

[ExcludeFromCodeCoverage]
public class BalanceService(IDatabaseFactory dbFactory) : IBalanceService
{
    public async Task<BalanceSchoolModel?> GetSchoolAsync(string urn, CancellationToken cancellationToken = default)
    {
        var builder = new BalanceSchoolDefaultCurrentQuery(Dimensions.Finance.Actuals)
            .WhereUrnEqual(urn);

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<BalanceSchoolModel>(builder, cancellationToken);
    }

    public async Task<(YearsModel?, IEnumerable<BalanceHistoryModel>)> GetSchoolHistoryAsync(string urn, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await conn.QueryYearsSchoolAsync(urn, cancellationToken);

        if (years == null)
        {
            return (null, []);
        }

        var historyBuilder = new BalanceSchoolDefaultQuery(dimension)
            .WhereUrnEqual(urn)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<BalanceHistoryModel>(historyBuilder, cancellationToken));
    }

    public async Task<BalanceTrustModel?> GetTrustAsync(string companyNumber, CancellationToken cancellationToken = default)
    {
        var builder = new BalanceTrustDefaultCurrentQuery(Dimensions.Finance.Actuals)
            .WhereCompanyNumberEqual(companyNumber);

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<BalanceTrustModel>(builder, cancellationToken);
    }

    public async Task<(YearsModel?, IEnumerable<BalanceHistoryModel>)> GetTrustHistoryAsync(string companyNumber, string dimension, CancellationToken cancellationToken = default)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await conn.QueryYearsTrustAsync(companyNumber, cancellationToken);

        if (years == null)
        {
            return (null, []);
        }

        var historyBuilder = new BalanceTrustDefaultQuery(dimension)
            .WhereCompanyNumberEqual(companyNumber)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<BalanceHistoryModel>(historyBuilder, cancellationToken));
    }

    public async Task<IEnumerable<BalanceTrustModel>> QueryTrustsAsync(string[] companyNumbers, string dimension, CancellationToken cancellationToken = default)
    {
        var builder = new BalanceTrustDefaultCurrentQuery(dimension)
            .WhereCompanyNumberIn(companyNumbers);

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<BalanceTrustModel>(builder, cancellationToken);
    }
}