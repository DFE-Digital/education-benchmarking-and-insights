using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Platform.Infrastructure.Sql;

namespace Platform.Api.Insight.Census;

public interface ICensusService
{
    Task<IEnumerable<CensusHistoryModel>> GetHistoryAsync(string urn);
    Task<IEnumerable<CensusModel>> QueryAsync(string[] urns);
    Task<CensusModel?> GetAsync(string urn);
}

public class CensusService : ICensusService
{
    private readonly IDatabaseFactory _dbFactory;

    public CensusService(IDatabaseFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<IEnumerable<CensusHistoryModel>> GetHistoryAsync(string urn)
    {
        const string sql = "SELECT * from SchoolCensusHistoric where URN = @URN";
        var parameters = new { URN = urn };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryAsync<CensusHistoryModel>(sql, parameters);
    }

    public async Task<IEnumerable<CensusModel>> QueryAsync(string[] urns)
    {
        const string sql = "SELECT * from SchoolCensus where URN IN @URNS";
        var parameters = new { URNS = urns };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryAsync<CensusModel>(sql, parameters);
    }

    public async Task<CensusModel?> GetAsync(string urn)
    {
        const string sql = "SELECT * from SchoolCensus where URN = @URN";
        var parameters = new { URN = urn };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<CensusModel>(sql, parameters);
    }
}