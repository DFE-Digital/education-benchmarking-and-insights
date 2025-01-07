using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Platform.Sql;

namespace Platform.Api.Insight.Balance;

public interface IBalanceService
{
    Task<BalanceSchoolModel?> GetSchoolAsync(string urn);
    Task<(BalanceYearsModel? years, IEnumerable<BalanceHistoryModel> rows)> GetSchoolHistoryAsync(string urn, string dimension = BalanceDimensions.Actuals);
    Task<BalanceTrustModel?> GetTrustAsync(string companyNumber);
    Task<(BalanceYearsModel? years, IEnumerable<BalanceHistoryModel> rows)> GetTrustHistoryAsync(string companyNumber, string dimension = BalanceDimensions.Actuals);
    Task<IEnumerable<BalanceTrustModel>> QueryTrustsAsync(string[] companyNumbers, string dimension = BalanceDimensions.Actuals);
}

public class BalanceService(IDatabaseFactory dbFactory) : IBalanceService
{
    public async Task<BalanceSchoolModel?> GetSchoolAsync(string urn)
    {
        var sql = GetSchoolSql(BalanceDimensions.Actuals);
        var parameters = new { URN = urn };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<BalanceSchoolModel>(sql, parameters);
    }

    public async Task<(BalanceYearsModel?, IEnumerable<BalanceHistoryModel>)> GetSchoolHistoryAsync(string urn, string dimension = BalanceDimensions.Actuals)
    {
        const string yearSql = "SELECT * FROM VW_YearsSchool WHERE URN = @URN";
        var yearParams = new { URN = urn };

        using var conn = await dbFactory.GetConnection();

        var years = await conn.QueryFirstOrDefaultAsync<BalanceYearsModel>(yearSql, yearParams);

        if (years == null)
        {
            return (null, Array.Empty<BalanceHistoryModel>());
        }

        var historySql = GetSchoolHistorySql(dimension);
        var historyParams = new { URN = urn, years.StartYear, years.EndYear };
        return (years, await conn.QueryAsync<BalanceHistoryModel>(historySql, historyParams));
    }

    public async Task<BalanceTrustModel?> GetTrustAsync(string companyNumber)
    {
        var sql = GetTrustSql(BalanceDimensions.Actuals);
        var parameters = new { CompanyNumber = companyNumber };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<BalanceTrustModel>(sql, parameters);
    }

    public async Task<(BalanceYearsModel?, IEnumerable<BalanceHistoryModel>)> GetTrustHistoryAsync(string companyNumber, string dimension = BalanceDimensions.Actuals)
    {
        const string yearSql = "SELECT * FROM VW_YearsTrust WHERE CompanyNumber = @CompanyNumber";
        var yearParams = new { CompanyNumber = companyNumber };

        using var conn = await dbFactory.GetConnection();

        var years = await conn.QueryFirstOrDefaultAsync<BalanceYearsModel>(yearSql, yearParams);

        if (years == null)
        {
            return (null, Array.Empty<BalanceHistoryModel>());
        }

        var historySql = GetTrustHistorySql(dimension);
        var historyParams = new { CompanyNumber = companyNumber, years.StartYear, years.EndYear };
        return (years, await conn.QueryAsync<BalanceHistoryModel>(historySql, historyParams));
    }

    public async Task<IEnumerable<BalanceTrustModel>> QueryTrustsAsync(string[] companyNumbers, string dimension = BalanceDimensions.Actuals)
    {
        var sql = QueryTrustsSql(dimension);
        var parameters = new { CompanyNumbers = companyNumbers };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<BalanceTrustModel>(sql, parameters);
    }

    private static string GetTrustSql(string dimension)
    {
        return dimension switch
        {
            BalanceDimensions.Actuals => "SELECT * FROM VW_BalanceTrustDefaultCurrentActual WHERE CompanyNumber = @CompanyNumber",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }

    private static string QueryTrustsSql(string dimension)
    {
        return dimension switch
        {
            BalanceDimensions.Actuals => "SELECT * FROM VW_BalanceTrustDefaultCurrentActual WHERE CompanyNumber IN @CompanyNumbers",
            BalanceDimensions.PerUnit => "SELECT * FROM VW_BalanceTrustDefaultCurrentActual WHERE CompanyNumber IN @CompanyNumbers",
            BalanceDimensions.PercentExpenditure => "SELECT * FROM VW_BalanceTrustDefaultCurrentActual WHERE CompanyNumber IN @CompanyNumbers",
            BalanceDimensions.PercentIncome => "SELECT * FROM VW_BalanceTrustDefaultCurrentActual WHERE CompanyNumber IN @CompanyNumbers",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }

    private static string GetTrustHistorySql(string dimension)
    {
        return dimension switch
        {
            BalanceDimensions.Actuals => "SELECT * FROM VW_BalanceTrustDefaultActual WHERE CompanyNumber = @CompanyNumber AND RunId BETWEEN @StartYear AND @EndYear",
            BalanceDimensions.PerUnit => "SELECT * FROM VW_BalanceTrustDefaultPerUnit WHERE CompanyNumber = @CompanyNumber AND RunId BETWEEN @StartYear AND @EndYear",
            BalanceDimensions.PercentExpenditure => "SELECT * FROM VW_BalanceTrustDefaultPercentExpenditure WHERE CompanyNumber = @CompanyNumber AND RunId BETWEEN @StartYear AND @EndYear",
            BalanceDimensions.PercentIncome => "SELECT * FROM VW_BalanceTrustDefaultPercentIncome WHERE CompanyNumber = @CompanyNumber AND RunId BETWEEN @StartYear AND @EndYear",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }

    private static string GetSchoolSql(string dimension)
    {
        return dimension switch
        {
            BalanceDimensions.Actuals => "SELECT * FROM VW_BalanceSchoolDefaultCurrentActual WHERE URN = @URN",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }

    private static string GetSchoolHistorySql(string dimension)
    {
        return dimension switch
        {
            BalanceDimensions.Actuals => "SELECT * FROM VW_BalanceSchoolDefaultActual WHERE URN = @URN AND RunId BETWEEN @StartYear AND @EndYear",
            BalanceDimensions.PerUnit => "SELECT * FROM VW_BalanceSchoolDefaultPerUnit WHERE URN = @URN AND RunId BETWEEN @StartYear AND @EndYear",
            BalanceDimensions.PercentExpenditure => "SELECT * FROM VW_BalanceSchoolDefaultPercentExpenditure WHERE URN = @URN AND RunId BETWEEN @StartYear AND @EndYear",
            BalanceDimensions.PercentIncome => "SELECT * FROM VW_BalanceSchoolDefaultPercentIncome WHERE URN = @URN AND RunId BETWEEN @StartYear AND @EndYear",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}