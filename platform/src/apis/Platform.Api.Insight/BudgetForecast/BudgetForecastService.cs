using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Platform.Infrastructure.Sql;
namespace Platform.Api.Insight.BudgetForecast;

public interface IBudgetForecastService
{
    Task<IEnumerable<BudgetForecastReturnModel>> GetBudgetForecastReturnsAsync(
        string companyNumber,
        string runType,
        string category,
        string? runId = null);
    Task<IEnumerable<BudgetForecastReturnMetricModel>> GetBudgetForecastReturnMetricsAsync(
        string companyNumber,
        string runType);
    Task<int?> GetBudgetForecastCurrentYearAsync(
        string companyNumber,
        string runType,
        string category);
}

public class BudgetForecastService : IBudgetForecastService
{
    private readonly IDatabaseFactory _dbFactory;

    public BudgetForecastService(IDatabaseFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<IEnumerable<BudgetForecastReturnModel>> GetBudgetForecastReturnsAsync(
        string companyNumber,
        string runType,
        string category,
        string? runId = null)
    {
        var builder = new SqlBuilder();
        var template = builder.AddTemplate("SELECT * from BudgetForecastReturn /**where**/");
        var parameters = new
        {
            CompanyNumber = companyNumber,
            RunType = runType,
            Category = category
        };

        builder.Where("CompanyNumber = @CompanyNumber and RunType = @RunType and Category = @Category", parameters);
        if (!string.IsNullOrWhiteSpace(runId))
        {
            builder.Where("RunId = @RunId", new
            {
                RunId = runId
            });
        }

        using var conn = await _dbFactory.GetConnection();
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

        using var conn = await _dbFactory.GetConnection();
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

        using var conn = await _dbFactory.GetConnection();
        return await conn.ExecuteScalarAsync<int?>(sql, parameters);
    }
}