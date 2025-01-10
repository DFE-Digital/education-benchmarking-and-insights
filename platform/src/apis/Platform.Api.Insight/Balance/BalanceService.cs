using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Insight.Balance;

public interface IBalanceService
{
    Task<BalanceSchoolModel?> GetSchoolAsync(string urn);
    Task<(BalanceYearsModel? years, IEnumerable<BalanceHistoryModel> rows)> GetSchoolHistoryAsync(string urn, string dimension = BalanceDimensions.Actuals);
    Task<BalanceTrustModel?> GetTrustAsync(string companyNumber);
    Task<(BalanceYearsModel?, IEnumerable<BalanceHistoryModel>)> GetTrustHistoryAsync(string companyNumber, string dimension = BalanceDimensions.Actuals);
    Task<IEnumerable<BalanceTrustModel>> QueryTrustsAsync(string[] companyNumbers, string dimension = BalanceDimensions.Actuals);
}

public class BalanceService(IDatabaseFactory dbFactory) : IBalanceService
{
    public async Task<BalanceSchoolModel?> GetSchoolAsync(string urn)
    {
        var builder = new BalanceSchoolDefaultCurrentQuery(BalanceDimensions.Actuals)
            .WhereUrnEqual(urn);
        
        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<BalanceSchoolModel>(builder);
    }

    public async Task<(BalanceYearsModel?, IEnumerable<BalanceHistoryModel>)> GetSchoolHistoryAsync(string urn, string dimension = BalanceDimensions.Actuals)
    {
        using var conn = await dbFactory.GetConnection();
        var yearsBuilder = new YearsSchoolQuery(urn);
        var years = await conn.QueryFirstOrDefaultAsync<BalanceYearsModel>(yearsBuilder);

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
        var builder = new BalanceTrustDefaultCurrentQuery(BalanceDimensions.Actuals)
            .WhereCompanyNumberEqual(companyNumber);
        
        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<BalanceTrustModel>(builder);
    }

    public async Task<(BalanceYearsModel?, IEnumerable<BalanceHistoryModel>)> GetTrustHistoryAsync(string companyNumber, string dimension = BalanceDimensions.Actuals)
    {
        using var conn = await dbFactory.GetConnection();
        var yearsBuilder = new YearsTrustQuery(companyNumber);
        var years = await conn.QueryFirstOrDefaultAsync<BalanceYearsModel>(yearsBuilder);

        if (years == null)
        {
            return (null, Array.Empty<BalanceHistoryModel>());
        }

        var historyBuilder = new BalanceTrustDefaultQuery(dimension)
            .WhereCompanyNumberEqual(companyNumber)
            .WhereRunIdBetween(years.StartYear, years.EndYear);
        
        return (years, await conn.QueryAsync<BalanceHistoryModel>(historyBuilder));
    }

    public async Task<IEnumerable<BalanceTrustModel>> QueryTrustsAsync(string[] companyNumbers, string dimension = BalanceDimensions.Actuals)
    {
        var builder = new BalanceTrustDefaultCurrentQuery(dimension)
            .WhereCompanyNumberIn(companyNumbers);
        
        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<BalanceTrustModel>(builder);
    }
}