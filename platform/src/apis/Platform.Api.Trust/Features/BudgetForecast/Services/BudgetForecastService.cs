using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Platform.Api.Trust.Features.BudgetForecast.Models;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Trust.Features.BudgetForecast.Services;

public interface IBudgetForecastService
{
    Task<IEnumerable<BudgetForecastReturnModelDto>> GetBudgetForecastReturnsAsync(
        string companyNumber,
        string runType,
        string category,
        string runId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<BudgetForecastReturnMetricModelDto>> GetBudgetForecastReturnMetricsAsync(
        string companyNumber,
        string runType,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<ActualReturnModelDto>> GetActualReturnsAsync(
        string companyNumber,
        string category,
        string runId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<ItSpendingResponse>> GetItSpendingAsync(
        string[] companyNumbers,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<ItSpendingForecastResponse>> GetItSpendingForecastAsync(
        string companyNumber,
        CancellationToken cancellationToken = default);
}

public class BudgetForecastService(IDatabaseFactory dbFactory) : IBudgetForecastService
{
    public async Task<IEnumerable<BudgetForecastReturnModelDto>> GetBudgetForecastReturnsAsync(
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
        return await conn.QueryAsync<BudgetForecastReturnModelDto>(template.RawSql, template.Parameters, cancellationToken);
    }

    public async Task<IEnumerable<BudgetForecastReturnMetricModelDto>> GetBudgetForecastReturnMetricsAsync(
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

        const string sql = "SELECT * from BudgetForecastReturnMetric where CompanyNumber = @CompanyNumber and RunType = @RunType AND RunId >= @StartYear AND RunId <= @EndYear";
        var parameters = new
        {
            CompanyNumber = companyNumber,
            RunType = runType,
            StartYear = year - 2,
            EndYear = year
        };

        return await conn.QueryAsync<BudgetForecastReturnMetricModelDto>(sql, parameters, cancellationToken);
    }

    public Task<IEnumerable<ActualReturnModelDto>> GetActualReturnsAsync(string companyNumber, string category, string runId, CancellationToken cancellationToken = default)
    {
        _ = int.TryParse(runId, out var year);

        return category.ToLower() switch
        {
            "revenue reserve" => GetActualRevenueReserveAsync(companyNumber, year, cancellationToken),
            _ => throw new ArgumentOutOfRangeException(nameof(category), "Unknown category")
        };
    }

    public async Task<IEnumerable<ItSpendingResponse>> GetItSpendingAsync(string[] companyNumbers, CancellationToken cancellationToken = default)
    {
        if (companyNumbers.Length == 0)
        {
            throw new ArgumentNullException(nameof(companyNumbers), $"{nameof(companyNumbers)} must be supplied");
        }

        var builder = new ItSpendTrustCurrentPreviousYearQuery();

        builder.WhereCompanyNumberIn(companyNumbers);

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<ItSpendingResponse>(builder, cancellationToken);
    }

    public async Task<IEnumerable<ItSpendingForecastResponse>> GetItSpendingForecastAsync(string companyNumber, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(companyNumber))
        {
            throw new ArgumentNullException(nameof(companyNumber), $"{nameof(companyNumber)} must be supplied");
        }

        var builder = new ItSpendTrustCurrentAllYearsQuery();

        builder.WhereCompanyNumberEqual(companyNumber);

        using var conn = await dbFactory.GetConnection();

        return await conn.QueryAsync<ItSpendingForecastResponse>(builder, cancellationToken);
    }

    private async Task<IEnumerable<ActualReturnModelDto>> GetActualRevenueReserveAsync(string companyNumber, int year, CancellationToken cancellationToken = default)
    {
        const string sql = "SELECT Year, RevenueReserve 'Value', TotalPupils FROM TrustBalanceHistoric WHERE CompanyNumber = @CompanyNumber AND Year >= @StartYear AND Year <= @EndYear";
        var parameters = new
        {
            CompanyNumber = companyNumber,
            StartYear = year - 2,
            EndYear = year
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<ActualReturnModelDto>(sql, parameters, cancellationToken);
    }
}