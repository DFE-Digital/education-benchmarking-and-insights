using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Platform.Sql;

namespace Platform.Api.Benchmark.UserData;

public interface IUserDataService
{
    Task<IEnumerable<UserData>> QueryAsync(string[] userIds, string? type = null, string? status = null, string? id = null, string? organisationId = null, string? organisationType = null);
}

[ExcludeFromCodeCoverage]
public class UserDataService(IDatabaseFactory dbFactory) : IUserDataService
{
    public async Task<IEnumerable<UserData>> QueryAsync(string[] userIds, string? type = null, string? status = null, string? id = null, string? organisationId = null, string? organisationType = null)
    {
        var template = Queries.GetUserDataByUserIds(userIds, type, status, id, organisationId, organisationType);

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<UserData>(template);
    }
}