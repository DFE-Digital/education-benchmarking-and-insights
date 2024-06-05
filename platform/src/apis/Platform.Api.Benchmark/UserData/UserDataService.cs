using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper;
using Platform.Infrastructure.Sql;

namespace Platform.Api.Benchmark.UserData;

public interface IUserDataService
{
    Task<IEnumerable<UserData>> QueryAsync(string userId, string? type = null, string? status = null, string? id = null);
}

[ExcludeFromCodeCoverage]
public class UserDataService : IUserDataService
{
    private readonly IDatabaseFactory _dbFactory;

    public UserDataService(IDatabaseFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<IEnumerable<UserData>> QueryAsync(string userId, string? type = null, string? status = null, string? id = null)
    {
        var builder = new SqlBuilder();
        var template = builder.AddTemplate("SELECT * from UserData /**where**/");

        builder.Where("UserId = @userId AND Status <> 'removed'", new { userId });

        if (!string.IsNullOrEmpty(type))
        {
            builder.Where("Type = @type", new { type });
        }

        if (!string.IsNullOrEmpty(status))
        {
            builder.Where("Status = @status", new { status });
        }

        if (!string.IsNullOrEmpty(id))
        {
            builder.Where("Id = @id", new { id });
        }

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryAsync<UserData>(template.RawSql, template.Parameters);
    }
}