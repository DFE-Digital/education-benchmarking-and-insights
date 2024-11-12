using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Platform.Sql;
namespace Platform.Api.Insight.Income;

public interface IIncomeService
{
    Task<SchoolIncomeModel?> GetSchoolAsync(string urn);
    Task<TrustIncomeModel?> GetTrustAsync(string companyNumber);
    Task<IEnumerable<SchoolIncomeHistoryModel>> GetSchoolHistoryAsync(string urn);
    Task<IEnumerable<TrustIncomeHistoryModel>> GetTrustHistoryAsync(string companyNumber);
    Task<IEnumerable<SchoolIncomeModel>> QuerySchoolsAsync(string[] urns, string? companyNumber, string? laCode, string? phase);
    Task<IEnumerable<TrustIncomeModel>> QueryTrustsAsync(string[] companyNumbers);
}

public class IncomeService(IDatabaseFactory dbFactory) : IIncomeService
{
    public async Task<SchoolIncomeModel?> GetSchoolAsync(string urn)
    {
        const string sql = "SELECT * FROM SchoolIncome WHERE URN = @URN";
        var parameters = new
        {
            URN = urn
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<SchoolIncomeModel>(sql, parameters);
    }

    public async Task<TrustIncomeModel?> GetTrustAsync(string companyNumber)
    {
        const string sql = "SELECT * FROM TrustIncome WHERE CompanyNumber = @CompanyNumber";
        var parameters = new
        {
            CompanyNumber = companyNumber
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<TrustIncomeModel>(sql, parameters);
    }

    public async Task<IEnumerable<SchoolIncomeHistoryModel>> GetSchoolHistoryAsync(string urn)
    {
        const string sql = "SELECT * FROM SchoolIncomeHistoric WHERE URN = @URN";
        var parameters = new
        {
            URN = urn
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<SchoolIncomeHistoryModel>(sql, parameters);
    }

    public async Task<IEnumerable<TrustIncomeHistoryModel>> GetTrustHistoryAsync(string companyNumber)
    {
        const string sql = "SELECT * FROM TrustIncomeHistoric WHERE CompanyNumber = @CompanyNumber";
        var parameters = new
        {
            CompanyNumber = companyNumber
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<TrustIncomeHistoryModel>(sql, parameters);
    }

    public async Task<IEnumerable<SchoolIncomeModel>> QuerySchoolsAsync(string[] urns, string? companyNumber, string? laCode, string? phase)
    {
        var builder = new SqlBuilder();
        var template = builder.AddTemplate("SELECT * from SchoolIncome /**where**/");
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
        return await conn.QueryAsync<SchoolIncomeModel>(template.RawSql, template.Parameters);
    }

    public async Task<IEnumerable<TrustIncomeModel>> QueryTrustsAsync(string[] companyNumbers)
    {
        const string sql = "SELECT * from TrustIncome where CompanyNumber IN @CompanyNumbers";
        var parameters = new
        {
            CompanyNumbers = companyNumbers
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<TrustIncomeModel>(sql, parameters);
    }
}