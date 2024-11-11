using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Platform.Sql;
namespace Platform.Api.Insight.Expenditure;

public interface IExpenditureService
{
    Task<SchoolExpenditureModel?> GetSchoolAsync(string urn);
    Task<TrustExpenditureModel?> GetTrustAsync(string companyNumber);
    Task<IEnumerable<SchoolExpenditureHistoryModel>> GetSchoolHistoryAsync(string urn);
    Task<IEnumerable<TrustExpenditureHistoryModel>> GetTrustHistoryAsync(string companyNumber);
    Task<IEnumerable<SchoolExpenditureModel>> QuerySchoolsAsync(string[] urns, string? companyNumber, string? laCode, string? phase);
    Task<IEnumerable<TrustExpenditureModel>> QueryTrustsAsync(string[] companyNumbers);
    Task<SchoolExpenditureModel?> GetCustomSchoolAsync(string urn, string identifier);
}

public class ExpenditureService(IDatabaseFactory dbFactory) : IExpenditureService
{
    public async Task<SchoolExpenditureModel?> GetSchoolAsync(string urn)
    {
        const string sql = "SELECT * FROM SchoolExpenditure WHERE URN = @URN";
        var parameters = new
        {
            URN = urn
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<SchoolExpenditureModel>(sql, parameters);
    }

    public async Task<SchoolExpenditureModel?> GetCustomSchoolAsync(string urn, string identifier)
    {
        const string sql = "SELECT * FROM SchoolExpenditureCustom WHERE URN = @URN AND RunId = @RunId";
        var parameters = new
        {
            URN = urn,
            RunId = identifier
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<SchoolExpenditureModel>(sql, parameters);
    }

    public async Task<TrustExpenditureModel?> GetTrustAsync(string companyNumber)
    {
        const string sql = "SELECT * FROM TrustExpenditure WHERE CompanyNumber = @CompanyNumber";
        var parameters = new
        {
            CompanyNumber = companyNumber
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<TrustExpenditureModel>(sql, parameters);
    }

    public async Task<IEnumerable<SchoolExpenditureHistoryModel>> GetSchoolHistoryAsync(string urn)
    {
        const string sql = "SELECT * FROM SchoolExpenditureHistoric WHERE URN = @URN";
        var parameters = new
        {
            URN = urn
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<SchoolExpenditureHistoryModel>(sql, parameters);
    }

    public async Task<IEnumerable<TrustExpenditureHistoryModel>> GetTrustHistoryAsync(string companyNumber)
    {
        const string sql = "SELECT * FROM TrustExpenditureHistoric WHERE CompanyNumber = @CompanyNumber";
        var parameters = new
        {
            CompanyNumber = companyNumber
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<TrustExpenditureHistoryModel>(sql, parameters);
    }

    public async Task<IEnumerable<SchoolExpenditureModel>> QuerySchoolsAsync(string[] urns, string? companyNumber, string? laCode, string? phase)
    {
        var builder = new SqlBuilder();
        var template = builder.AddTemplate("SELECT * from SchoolExpenditure /**where**/");
        if (urns.Length != 0)
        {
            builder.Where("URN IN @URNS", new
            {
                URNS = urns
            });
        }
        else if (!string.IsNullOrWhiteSpace(companyNumber))
        {
            builder.Where("TrustCompanyNumber = @CompanyNumber AND OverallPhase = @Phase", new
            {
                CompanyNumber = companyNumber,
                Phase = phase
            });
        }
        else if (!string.IsNullOrWhiteSpace(laCode))
        {
            builder.Where("LaCode = @LaCode AND OverallPhase = @Phase", new
            {
                LaCode = laCode,
                Phase = phase
            });
        }
        else
        {
            throw new ArgumentNullException(nameof(urns), $"{nameof(urns)} or {nameof(companyNumber)} or {nameof(laCode)} must be supplied");
        }

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<SchoolExpenditureModel>(template.RawSql, template.Parameters);
    }

    public async Task<IEnumerable<TrustExpenditureModel>> QueryTrustsAsync(string[] companyNumbers)
    {
        const string sql = "SELECT * from TrustExpenditure where CompanyNumber IN @CompanyNumbers";
        var parameters = new
        {
            CompanyNumbers = companyNumbers
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<TrustExpenditureModel>(sql, parameters);
    }
}