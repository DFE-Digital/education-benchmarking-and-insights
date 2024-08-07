using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Platform.Infrastructure.Sql;
namespace Platform.Api.Insight.Balance;

public interface IBalanceService
{
    Task<SchoolBalanceModel?> GetSchoolAsync(string urn);
    Task<TrustBalanceModel?> GetTrustAsync(string companyNumber);
    Task<IEnumerable<SchoolBalanceHistoryModel>> GetSchoolHistoryAsync(string urn);
    Task<IEnumerable<TrustBalanceHistoryModel>> GetTrustHistoryAsync(string companyNumber);
    Task<IEnumerable<SchoolBalanceModel>> QuerySchoolsAsync(string[] urns);
    Task<IEnumerable<TrustBalanceModel>> QueryTrustsAsync(string[] companyNumbers);
}

public class BalanceService(IDatabaseFactory dbFactory) : IBalanceService
{
    public async Task<SchoolBalanceModel?> GetSchoolAsync(string urn)
    {
        const string sql = "SELECT * FROM SchoolBalance WHERE URN = @URN";
        var parameters = new
        {
            URN = urn
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<SchoolBalanceModel>(sql, parameters);
    }

    public async Task<TrustBalanceModel?> GetTrustAsync(string companyNumber)
    {
        const string sql = "SELECT * FROM TrustBalance WHERE CompanyNumber = @CompanyNumber";
        var parameters = new
        {
            CompanyNumber = companyNumber
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<TrustBalanceModel>(sql, parameters);
    }

    public async Task<IEnumerable<SchoolBalanceHistoryModel>> GetSchoolHistoryAsync(string urn)
    {
        const string sql = "SELECT * FROM SchoolBalanceHistoric WHERE URN = @URN";
        var parameters = new
        {
            URN = urn
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<SchoolBalanceHistoryModel>(sql, parameters);
    }

    public async Task<IEnumerable<TrustBalanceHistoryModel>> GetTrustHistoryAsync(string companyNumber)
    {
        const string sql = "SELECT * FROM TrustBalanceHistoric WHERE CompanyNumber = @CompanyNumber";
        var parameters = new
        {
            CompanyNumber = companyNumber
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<TrustBalanceHistoryModel>(sql, parameters);
    }

    public async Task<IEnumerable<SchoolBalanceModel>> QuerySchoolsAsync(string[] urns)
    {
        const string sql = "SELECT * from SchoolBalance where URN IN @URNS";
        var parameters = new
        {
            URNS = urns
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<SchoolBalanceModel>(sql, parameters);
    }

    public async Task<IEnumerable<TrustBalanceModel>> QueryTrustsAsync(string[] companyNumbers)
    {
        const string sql = "SELECT * from TrustBalance where CompanyNumber IN @CompanyNumbers";
        var parameters = new
        {
            CompanyNumbers = companyNumbers
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<TrustBalanceModel>(sql, parameters);
    }
}