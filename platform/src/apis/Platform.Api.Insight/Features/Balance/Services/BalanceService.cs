using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Platform.Api.Insight.Features.Balance.Models;
using Platform.Api.Insight.Shared;
using Platform.Domain;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Insight.Features.Balance.Services;

public interface IBalanceService
{
    Task<BalanceSchoolModel?> GetSchoolAsync(string urn);
    Task<(YearsModel? years, IEnumerable<BalanceHistoryModel> rows)> GetSchoolHistoryAsync(string urn, string dimension);
    Task<BalanceTrustModel?> GetTrustAsync(string companyNumber);
    Task<(YearsModel?, IEnumerable<BalanceHistoryModel>)> GetTrustHistoryAsync(string companyNumber, string dimension);
    Task<IEnumerable<BalanceTrustModel>> QueryTrustsAsync(string[] companyNumbers, string dimension);
}

[ExcludeFromCodeCoverage]
public class BalanceService(IDatabaseFactory dbFactory) : IBalanceService
{
    public async Task<BalanceSchoolModel?> GetSchoolAsync(string urn)
    {
        var builder = new BalanceSchoolDefaultCurrentQuery(Dimensions.Finance.Actuals)
            .WhereUrnEqual(urn);

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<BalanceSchoolModel>(builder);
    }

    public async Task<(YearsModel?, IEnumerable<BalanceHistoryModel>)> GetSchoolHistoryAsync(string urn, string dimension)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await conn.QueryYearsSchoolAsync(urn);

        if (years == null)
        {
            return (null, Array.Empty<BalanceHistoryModel>());
        }

        var historyBuilder = new BalanceSchoolDefaultQuery(dimension)
            .WhereUrnEqual(urn)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<BalanceHistoryModel>(historyBuilder));
    }

    public async Task<BalanceTrustModel?> GetTrustAsync(string companyNumber)
    {
        var builder = new BalanceTrustDefaultCurrentQuery(Dimensions.Finance.Actuals)
            .WhereCompanyNumberEqual(companyNumber);

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<BalanceTrustModel>(builder);
    }

    public async Task<(YearsModel?, IEnumerable<BalanceHistoryModel>)> GetTrustHistoryAsync(string companyNumber, string dimension)
    {
        using var conn = await dbFactory.GetConnection();
        var years = await conn.QueryYearsTrustAsync(companyNumber);

        if (years == null)
        {
            return (null, Array.Empty<BalanceHistoryModel>());
        }

        var historyBuilder = new BalanceTrustDefaultQuery(dimension)
            .WhereCompanyNumberEqual(companyNumber)
            .WhereRunIdBetween(years.StartYear, years.EndYear);

        return (years, await conn.QueryAsync<BalanceHistoryModel>(historyBuilder));
    }

    public async Task<IEnumerable<BalanceTrustModel>> QueryTrustsAsync(string[] companyNumbers, string dimension)
    {
        var builder = new BalanceTrustDefaultCurrentQuery(dimension)
            .WhereCompanyNumberIn(companyNumbers);

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<BalanceTrustModel>(builder);
    }
}