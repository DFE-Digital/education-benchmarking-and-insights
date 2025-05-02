using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Platform.Api.Insight.Features.BudgetForecast.Models;
using Platform.Sql;

namespace Platform.Api.Insight.Features.BudgetForecast.Services;

public interface IBudgetForecastService
{
    Task<IEnumerable<BudgetForecastReturnModel>> GetBudgetForecastReturnsAsync(
        string companyNumber,
        string runType,
        string category,
        string runId,
        CancellationToken cancellationToken = default);
    Task<IEnumerable<BudgetForecastReturnMetricModel>> GetBudgetForecastReturnMetricsAsync(
        string companyNumber,
        string runType,
        CancellationToken cancellationToken = default);
    Task<int?> GetBudgetForecastCurrentYearAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ActualReturnModel>> GetActualReturnsAsync(
        string companyNumber,
        string category,
        string runId,
        CancellationToken cancellationToken = default);
}

public class BudgetForecastService(IDatabaseFactory dbFactory) : IBudgetForecastService
{
    public async Task<IEnumerable<BudgetForecastReturnModel>> GetBudgetForecastReturnsAsync(
        string companyNumber,
        string runType,
        string category,
        string runId,
        CancellationToken cancellationToken = default)
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
        return await conn.QueryAsync<BudgetForecastReturnModel>(template.RawSql, template.Parameters, cancellationToken);
    }

    public async Task<IEnumerable<BudgetForecastReturnMetricModel>> GetBudgetForecastReturnMetricsAsync(
        string companyNumber,
        string runType,
        CancellationToken cancellationToken = default)
    {
        const string paramSql = "SELECT Value FROM Parameters WHERE Name = @Name";
        using var conn = await dbFactory.GetConnection();
        var year = await conn.QueryFirstOrDefaultAsync<int>(paramSql, new
        {
            Name = "LatestBFRYear"
        }, cancellationToken);

        const string sql = "SELECT * from BudgetForecastReturnMetric where CompanyNumber = @CompanyNumber and RunType = @RunType AND Year >= @StartYear AND Year <= @EndYear";
        var parameters = new
        {
            CompanyNumber = companyNumber,
            RunType = runType,
            StartYear = year - 2,
            EndYear = year
        };


        return await conn.QueryAsync<BudgetForecastReturnMetricModel>(sql, parameters, cancellationToken);
    }

    public async Task<int?> GetBudgetForecastCurrentYearAsync(CancellationToken cancellationToken = default)
    {
        const string sql = "SELECT Value FROM Parameters WHERE Name = @Name";
        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<int?>(sql, new
        {
            Name = "LatestBFRYear"
        }, cancellationToken);
    }

    public Task<IEnumerable<ActualReturnModel>> GetActualReturnsAsync(string companyNumber, string category, string runId, CancellationToken cancellationToken = default)
    {
        _ = int.TryParse(runId, out var year);

        return category.ToLower() switch
        {
            "revenue reserve" => GetActualRevenueReserveAsync(companyNumber, year, cancellationToken),
            _ => throw new ArgumentOutOfRangeException(nameof(category), "Unknown category")
        };
    }

    private async Task<IEnumerable<ActualReturnModel>> GetActualRevenueReserveAsync(string companyNumber, int year, CancellationToken cancellationToken = default)
    {
        const string sql = "SELECT Year, RevenueReserve 'Value', TotalPupils FROM TrustBalanceHistoric WHERE CompanyNumber = @CompanyNumber AND Year >= @StartYear AND Year <= @EndYear";
        var parameters = new
        {
            CompanyNumber = companyNumber,
            StartYear = year - 2,
            EndYear = year
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<ActualReturnModel>(sql, parameters, cancellationToken);
    }
}