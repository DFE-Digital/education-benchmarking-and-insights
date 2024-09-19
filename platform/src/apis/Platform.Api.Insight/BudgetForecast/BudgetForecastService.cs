using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Platform.Sql;
namespace Platform.Api.Insight.BudgetForecast;

public interface IBudgetForecastService
{
    Task<IEnumerable<BudgetForecastReturnModel>> GetBudgetForecastReturnsAsync(
        string companyNumber,
        string runType,
        string category,
        string runId);
    Task<IEnumerable<BudgetForecastReturnMetricModel>> GetBudgetForecastReturnMetricsAsync(
        string companyNumber,
        string runType);
    Task<int?> GetBudgetForecastCurrentYearAsync(
        string companyNumber,
        string runType,
        string category);

    Task<IEnumerable<ActualReturnModel>> GetActualReturnsAsync(
        string companyNumber,
        string category,
        string runId);
}

public class BudgetForecastService(IDatabaseFactory dbFactory) : IBudgetForecastService
{
    public async Task<IEnumerable<BudgetForecastReturnModel>> GetBudgetForecastReturnsAsync(
        string companyNumber,
        string runType,
        string category,
        string runId)
    {
        var builder = new SqlBuilder();
        var template = builder.AddTemplate("SELECT * from BudgetForecastReturn /**where**/");
        var parameters = new
        {
            CompanyNumber = companyNumber,
            RunType = runType,
            Category = category,
            RunId = runId
        };

        builder.Where("CompanyNumber = @CompanyNumber AND RunType = @RunType AND Category = @Category AND RunId = @RunId", parameters);

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<BudgetForecastReturnModel>(template.RawSql, template.Parameters);
    }

    public async Task<IEnumerable<BudgetForecastReturnMetricModel>> GetBudgetForecastReturnMetricsAsync(
        string companyNumber,
        string runType)
    {
        const string sql = "SELECT * from BudgetForecastReturnMetric where CompanyNumber = @CompanyNumber and RunType = @RunType";
        var parameters = new
        {
            CompanyNumber = companyNumber,
            RunType = runType
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<BudgetForecastReturnMetricModel>(sql, parameters);
    }

    public async Task<int?> GetBudgetForecastCurrentYearAsync(string companyNumber, string runType, string category)
    {
        // for 'default' rows the `RunId` will be numeric and the year, but this won't necessarily always be the case
        if (runType != "default")
        {
            return null;
        }

        const string sql = "select convert(int, max(RunId)) from BudgetForecastReturn where CompanyNumber = @CompanyNumber and RunType = @RunType and Category = @Category";
        var parameters = new
        {
            CompanyNumber = companyNumber,
            RunType = runType,
            Category = category
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.ExecuteScalarAsync<int?>(sql, parameters);
    }

    public Task<IEnumerable<ActualReturnModel>> GetActualReturnsAsync(string companyNumber, string category, string runId)
    {
        _ = int.TryParse(runId, out var year);

        return category.ToLower() switch
        {
            "revenue reserve" => GetActualRevenueReserveAsync(companyNumber, year),
            _ => throw new ArgumentOutOfRangeException(nameof(category), "Unknown category")
        };
    }

    private async Task<IEnumerable<ActualReturnModel>> GetActualRevenueReserveAsync(string companyNumber, int year)
    {
        const string sql = "SELECT Year, RevenueReserve 'Value', TotalPupils FROM TrustBalanceHistoric WHERE CompanyNumber = @CompanyNumber AND Year >= @StartYear AND Year <= @EndYear";
        var parameters = new
        {
            CompanyNumber = companyNumber,
            StartYear = year - 2,
            EndYear = year
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<ActualReturnModel>(sql, parameters);
    }
}