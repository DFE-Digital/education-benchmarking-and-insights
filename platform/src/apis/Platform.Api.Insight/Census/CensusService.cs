using System.Collections.Generic;
using System.Threading.Tasks;
using Platform.Sql;
namespace Platform.Api.Insight.Census;

public interface ICensusService
{
    Task<IEnumerable<CensusHistoryModel>> GetHistoryAsync(string urn);
    Task<IEnumerable<CensusModel>> QueryAsync(string[] urns);
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

    public async Task<IEnumerable<CensusModel>> QueryAsync(string[] urns)
    {
        var template = Queries.GetCensus(urns);
        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<CensusModel>(template);
    }

    public async Task<CensusModel?> GetAsync(string urn)
    {
        var template = Queries.GetCensus(urn);
        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<CensusModel>(template);
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