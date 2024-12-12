using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Platform.Sql;
namespace Platform.Api.Insight.Census;

public interface ICensusService
{
    Task<IEnumerable<CensusHistoryModel>> GetHistoryAsync(string urn);
    Task<IEnumerable<CensusHistoryModel>> GetHistoryAvgComparatorSetAsync(string urn, string dimension);
    Task<IEnumerable<CensusHistoryModel>> GetHistoryAvgNationalAsync(string dimension, string overallPhase, string financeType);
    Task<IEnumerable<CensusModel>> QueryAsync(string[] urns, string? companyNumber, string? laCode, string? phase);
    Task<CensusModel?> GetAsync(string urn);
    Task<CensusModel?> GetCustomAsync(string urn, string identifier);
}

public class CensusService(IDatabaseFactory dbFactory) : ICensusService
{
    public async Task<IEnumerable<CensusHistoryModel>> GetHistoryAsync(string urn)
    {
        const string sql = "SELECT * from SchoolCensusHistoric WHERE URN = @URN";
        var parameters = new
        {
            URN = urn
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<CensusHistoryModel>(sql, parameters);
    }

    public async Task<IEnumerable<CensusHistoryModel>> GetHistoryAvgComparatorSetAsync(string urn, string dimension)
    {
        var parameters = new
        {
            URN = urn
        };

        var sourceName = dimension switch
        {
            CensusDimensions.Total => "SchoolCensusAvgComparatorSet",
            CensusDimensions.HeadcountPerFte => "SchoolCensusAvgPerFteComparatorSet",
            CensusDimensions.PercentWorkforce => "SchoolCensusAvgPercentageOfWorkforceFteComparatorSet",
            CensusDimensions.PupilsPerStaffRole => "SchoolCensusAvgPupilsPerStaffComparatorSet",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension))
        };

        var sql = $"SELECT * FROM {sourceName} WHERE URN = @URN";


        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<CensusHistoryModel>(sql, parameters);
    }

    public async Task<IEnumerable<CensusHistoryModel>> GetHistoryAvgNationalAsync(string dimension, string overallPhase, string financeType)
    {
        var parameters = new
        {
            OverallPhase = overallPhase,
            FinanceType = financeType
        };

        var sourceName = dimension switch
        {
            CensusDimensions.Total => "SchoolCensusAvgHistoric",
            CensusDimensions.HeadcountPerFte => "SchoolCensusAvgPerFteHistoric",
            CensusDimensions.PercentWorkforce => "SchoolCensusAvgPercentageOfWorkforceFteHistoric",
            CensusDimensions.PupilsPerStaffRole => "SchoolCensusAvgPupilsPerStaffHistoric",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension))
        };

        var sql = $"SELECT * FROM {sourceName} WHERE FinanceType = @FinanceType AND OverallPhase = @OverallPhase";

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<CensusHistoryModel>(sql, parameters);
    }

    public async Task<IEnumerable<CensusModel>> QueryAsync(string[] urns, string? companyNumber, string? laCode, string? phase)
    {
        var builder = new SqlBuilder();
        var template = builder.AddTemplate("SELECT * from SchoolCensus /**where**/");
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
        return await conn.QueryAsync<CensusModel>(template.RawSql, template.Parameters);
    }

    public async Task<CensusModel?> GetAsync(string urn)
    {
        const string sql = "SELECT * from SchoolCensus WHERE URN = @URN";
        var parameters = new
        {
            URN = urn
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<CensusModel>(sql, parameters);
    }

    public async Task<CensusModel?> GetCustomAsync(string urn, string identifier)
    {
        const string sql = "SELECT * from SchoolCensusCustom WHERE URN = @URN AND RunId = @RunId";
        var parameters = new
        {
            URN = urn,
            RunId = identifier
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<CensusModel>(sql, parameters);
    }
}