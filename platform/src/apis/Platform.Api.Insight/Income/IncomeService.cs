using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Platform.Infrastructure.Sql;

namespace Platform.Api.Insight.Income;

public interface IIncomeService
{
    Task<SchoolIncomeModel?> GetSchoolAsync(string urn);
    Task<TrustIncomeModel?> GetTrustAsync(string companyNumber);
    Task<IEnumerable<SchoolIncomeHistoryModel>> GetSchoolHistoryAsync(string urn);
    Task<IEnumerable<TrustIncomeHistoryModel>> GetTrustHistoryAsync(string companyNumber);
    Task<IEnumerable<SchoolIncomeModel>> QuerySchoolsAsync(string[] urns);
    Task<IEnumerable<TrustIncomeModel>> QueryTrustsAsync(string[] companyNumbers);
}

public class IncomeService : IIncomeService
{
    private readonly IDatabaseFactory _dbFactory;

    public IncomeService(IDatabaseFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<SchoolIncomeModel?> GetSchoolAsync(string urn)
    {
        const string sql = "SELECT * FROM SchoolIncome WHERE URN = @URN";
        var parameters = new { URN = urn };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<SchoolIncomeModel>(sql, parameters);
    }

    public async Task<TrustIncomeModel?> GetTrustAsync(string companyNumber)
    {
        const string sql = "SELECT * FROM TrustIncome WHERE CompanyNumber = @CompanyNumber";
        var parameters = new { CompanyNumber = companyNumber };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<TrustIncomeModel>(sql, parameters);
    }

    public async Task<IEnumerable<SchoolIncomeHistoryModel>> GetSchoolHistoryAsync(string urn)
    {
        const string sql = "SELECT * FROM SchoolIncomeHistoric WHERE URN = @URN";
        var parameters = new { URN = urn };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryAsync<SchoolIncomeHistoryModel>(sql, parameters);
    }

    public async Task<IEnumerable<TrustIncomeHistoryModel>> GetTrustHistoryAsync(string companyNumber)
    {
        const string sql = "SELECT * FROM TrustIncomeHistoric WHERE CompanyNumber = @CompanyNumber";
        var parameters = new { CompanyNumber = companyNumber };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryAsync<TrustIncomeHistoryModel>(sql, parameters);
    }

    public async Task<IEnumerable<SchoolIncomeModel>> QuerySchoolsAsync(string[] urns)
    {
        const string sql = "SELECT * from SchoolIncome where URN IN @URNS";
        var parameters = new { URNS = urns };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryAsync<SchoolIncomeModel>(sql, parameters);
    }

    public async Task<IEnumerable<TrustIncomeModel>> QueryTrustsAsync(string[] companyNumbers)
    {
        const string sql = "SELECT * from TrustIncome where CompanyNumber IN @CompanyNumbers";
        var parameters = new { CompanyNumbers = companyNumbers };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryAsync<TrustIncomeModel>(sql, parameters);
    }
}