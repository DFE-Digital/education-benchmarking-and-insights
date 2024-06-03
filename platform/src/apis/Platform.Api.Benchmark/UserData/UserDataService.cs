using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper;
using Platform.Infrastructure.Sql;

namespace Platform.Api.Benchmark.UserData;

public interface IUserDataService
{
    Task<IEnumerable<UserData>> QueryAsync(string userId);
}

[ExcludeFromCodeCoverage]
public class UserDataService : IUserDataService
{
    private readonly IDatabaseFactory _dbFactory;

    public UserDataService(IDatabaseFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<IEnumerable<UserData>> QueryAsync(string userId)
    {
        const string sql = "SELECT * from UserData where UserId = @UserId";
        var parameters = new { UserId = userId };
        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryAsync<UserData>(sql, parameters);
    }
}