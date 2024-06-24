using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Platform.Infrastructure.Sql;

namespace Platform.Api.Insight.Expenditure;

public interface IExpenditureService
{
    Task<SchoolExpenditureModel?> GetSchoolAsync(string urn);
    Task<TrustExpenditureModel?> GetTrustAsync(string companyNumber);
    Task<IEnumerable<SchoolExpenditureHistoryModel>> GetSchoolHistoryAsync(string urn);
    Task<IEnumerable<TrustExpenditureHistoryModel>> GetTrustHistoryAsync(string companyNumber);
    Task<IEnumerable<SchoolExpenditureModel>> QuerySchoolsAsync(string[] urns);
    Task<IEnumerable<TrustExpenditureModel>> QueryTrustsAsync(string[] companyNumbers);
    Task<SchoolExpenditureModel?> GetCustomSchoolAsync(string urn, string identifier);
}

public class ExpenditureService : IExpenditureService
{
    private readonly IDatabaseFactory _dbFactory;

    public ExpenditureService(IDatabaseFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<SchoolExpenditureModel?> GetSchoolAsync(string urn)
    {
        const string sql = "SELECT * FROM SchoolExpenditure WHERE URN = @URN";
        var parameters = new { URN = urn };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<SchoolExpenditureModel>(sql, parameters);
    }

    public async Task<SchoolExpenditureModel?> GetCustomSchoolAsync(string urn, string identifier)
    {
        const string sql = "SELECT * FROM SchoolExpenditureCustom WHERE URN = @URN AND RunId = @RunId";
        var parameters = new { URN = urn, RunId = identifier };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<SchoolExpenditureModel>(sql, parameters);
    }

    public async Task<TrustExpenditureModel?> GetTrustAsync(string companyNumber)
    {
        const string sql = "SELECT * FROM TrustExpenditure WHERE CompanyNumber = @CompanyNumber";
        var parameters = new { CompanyNumber = companyNumber };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<TrustExpenditureModel>(sql, parameters);
    }

    public async Task<IEnumerable<SchoolExpenditureHistoryModel>> GetSchoolHistoryAsync(string urn)
    {
        const string sql = "SELECT * FROM SchoolExpenditureHistoric WHERE URN = @URN";
        var parameters = new { URN = urn };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryAsync<SchoolExpenditureHistoryModel>(sql, parameters);
    }

    public async Task<IEnumerable<TrustExpenditureHistoryModel>> GetTrustHistoryAsync(string companyNumber)
    {
        const string sql = "SELECT * FROM TrustExpenditureHistoric WHERE CompanyNumber = @CompanyNumber";
        var parameters = new { CompanyNumber = companyNumber };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryAsync<TrustExpenditureHistoryModel>(sql, parameters);
    }

    public async Task<IEnumerable<SchoolExpenditureModel>> QuerySchoolsAsync(string[] urns)
    {
        const string sql = "SELECT * from SchoolExpenditure where URN IN @URNS";
        var parameters = new { URNS = urns };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryAsync<SchoolExpenditureModel>(sql, parameters);
    }

    public async Task<IEnumerable<TrustExpenditureModel>> QueryTrustsAsync(string[] companyNumbers)
    {
        const string sql = "SELECT * from TrustExpenditure where CompanyNumber IN @CompanyNumbers";
        var parameters = new { CompanyNumbers = companyNumbers };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryAsync<TrustExpenditureModel>(sql, parameters);
    }
}