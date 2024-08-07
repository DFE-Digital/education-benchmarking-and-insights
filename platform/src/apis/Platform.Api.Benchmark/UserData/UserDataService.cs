using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper;
using Platform.Infrastructure.Sql;
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
        var builder = new SqlBuilder();
        var template = builder.AddTemplate("SELECT * from UserData /**where**/");

        builder.Where("UserId IN @userIds AND Status IN ('pending','complete')", new
        {
            userIds
        });

        if (!string.IsNullOrEmpty(organisationId))
        {
            builder.Where("OrganisationId = @organisationId", new
            {
                organisationId
            });
        }

        if (!string.IsNullOrEmpty(organisationId))
        {
            builder.Where("OrganisationType = @organisationType", new
            {
                organisationType
            });
        }

        if (!string.IsNullOrEmpty(type))
        {
            builder.Where("Type = @type", new
            {
                type
            });
        }

        if (!string.IsNullOrEmpty(status))
        {
            builder.Where("Status = @status", new
            {
                status
            });
        }

        if (!string.IsNullOrEmpty(id))
        {
            builder.Where("Id = @id", new
            {
                id
            });
        }

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<UserData>(template.RawSql, template.Parameters);
    }
}