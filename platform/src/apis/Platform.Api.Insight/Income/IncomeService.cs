using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Platform.Sql;

namespace Platform.Api.Insight.Income;

public interface IIncomeService
{
    Task<IncomeSchoolModel?> GetSchoolAsync(string urn);
    Task<(IncomeYearsModel? years, IEnumerable<IncomeHistoryModel> rows)> GetSchoolHistoryAsync(string urn, string dimension = IncomeDimensions.Actuals);
    Task<(IncomeYearsModel?, IEnumerable<IncomeHistoryModel>)> GetTrustHistoryAsync(string companyNumber, string dimension = IncomeDimensions.Actuals);
}

public class IncomeService(IDatabaseFactory dbFactory) : IIncomeService
{
    public async Task<IncomeSchoolModel?> GetSchoolAsync(string urn)
    {
        var sql = GetSchoolSql(IncomeDimensions.Actuals);
        var parameters = new { URN = urn };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<IncomeSchoolModel>(sql, parameters);
    }

    public async Task<(IncomeYearsModel?, IEnumerable<IncomeHistoryModel>)> GetSchoolHistoryAsync(string urn, string dimension = IncomeDimensions.Actuals)
    {
        const string yearSql = "SELECT * FROM VW_YearsSchool WHERE URN = @URN";
        var yearParams = new { URN = urn };

        using var conn = await dbFactory.GetConnection();

        var years = await conn.QueryFirstOrDefaultAsync<IncomeYearsModel>(yearSql, yearParams);

        if (years == null)
        {
            return (null, Array.Empty<IncomeHistoryModel>());
        }

        var historySql = GetSchoolHistorySql(dimension);
        var historyParams = new { URN = urn, years.StartYear, years.EndYear };
        return (years, await conn.QueryAsync<IncomeHistoryModel>(historySql, historyParams));
    }

    public async Task<(IncomeYearsModel?, IEnumerable<IncomeHistoryModel>)> GetTrustHistoryAsync(string companyNumber, string dimension = IncomeDimensions.Actuals)
    {
        const string yearSql = "SELECT * FROM VW_YearsTrust WHERE CompanyNumber = @CompanyNumber";
        var yearParams = new { CompanyNumber = companyNumber };

        using var conn = await dbFactory.GetConnection();

        var years = await conn.QueryFirstOrDefaultAsync<IncomeYearsModel>(yearSql, yearParams);

        if (years == null)
        {
            return (null, Array.Empty<IncomeHistoryModel>());
        }

        var historySql = GetTrustHistorySql(dimension);
        var historyParams = new { CompanyNumber = companyNumber, years.StartYear, years.EndYear };
        return (years, await conn.QueryAsync<IncomeHistoryModel>(historySql, historyParams));
    }

    private static string GetTrustHistorySql(string dimension)
    {
        return dimension switch
        {
            IncomeDimensions.Actuals => "SELECT * FROM VW_IncomeTrustDefaultActual WHERE CompanyNumber = @CompanyNumber AND RunId BETWEEN @StartYear AND @EndYear",
            IncomeDimensions.PerUnit => "SELECT * FROM VW_IncomeTrustDefaultPerUnit WHERE CompanyNumber = @CompanyNumber AND RunId BETWEEN @StartYear AND @EndYear",
            IncomeDimensions.PercentExpenditure => "SELECT * FROM VW_IncomeTrustDefaultPercentExpenditure WHERE CompanyNumber = @CompanyNumber AND RunId BETWEEN @StartYear AND @EndYear",
            IncomeDimensions.PercentIncome => "SELECT * FROM VW_IncomeTrustDefaultPercentIncome WHERE CompanyNumber = @CompanyNumber AND RunId BETWEEN @StartYear AND @EndYear",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }

    private static string GetSchoolSql(string dimension)
    {
        return dimension switch
        {
            IncomeDimensions.Actuals => "SELECT * FROM VW_IncomeSchoolDefaultCurrentActual WHERE URN = @URN",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }

    private static string GetSchoolHistorySql(string dimension)
    {
        return dimension switch
        {
            IncomeDimensions.Actuals => "SELECT * FROM VW_IncomeSchoolDefaultActual WHERE URN = @URN AND RunId BETWEEN @StartYear AND @EndYear",
            IncomeDimensions.PerUnit => "SELECT * FROM VW_IncomeSchoolDefaultPerUnit WHERE URN = @URN AND RunId BETWEEN @StartYear AND @EndYear",
            IncomeDimensions.PercentExpenditure => "SELECT * FROM VW_IncomeSchoolDefaultPercentExpenditure WHERE URN = @URN AND RunId BETWEEN @StartYear AND @EndYear",
            IncomeDimensions.PercentIncome => "SELECT * FROM VW_IncomeSchoolDefaultPercentIncome WHERE URN = @URN AND RunId BETWEEN @StartYear AND @EndYear",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}