using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Platform.Infrastructure.Sql;
namespace Platform.Api.Insight.BudgetForecast;

public interface IBudgetForecastService
{
    Task<IEnumerable<BudgetForecastReturnModel>> GetBudgetForecastReturnsAsync(string companyNumber);
    Task<IEnumerable<BudgetForecastReturnMetricModel>> GetBudgetForecastReturnMetricsAsync(string companyNumber);
}

public class BudgetForecastService : IBudgetForecastService
{
    private readonly IDatabaseFactory _dbFactory;

    public BudgetForecastService(IDatabaseFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<IEnumerable<BudgetForecastReturnModel>> GetBudgetForecastReturnsAsync(string companyNumber)
    {
        const string sql = "SELECT * from BudgetForecastReturn where CompanyNumber = @CompanyNumber";
        var parameters = new { CompanyNumber = companyNumber };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryAsync<BudgetForecastReturnModel>(sql, parameters);
    }

    public async Task<IEnumerable<BudgetForecastReturnMetricModel>> GetBudgetForecastReturnMetricsAsync(string companyNumber)
    {
        const string sql = "SELECT * from BudgetForecastReturnMetric where CompanyNumber = @CompanyNumber";
        var parameters = new { CompanyNumber = companyNumber };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryAsync<BudgetForecastReturnMetricModel>(sql, parameters);
    }
}