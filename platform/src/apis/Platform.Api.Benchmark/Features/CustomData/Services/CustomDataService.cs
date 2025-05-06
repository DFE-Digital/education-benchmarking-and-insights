using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Platform.Api.Benchmark.Features.CustomData.Models;
using Platform.Domain;
using Platform.Sql;

namespace Platform.Api.Benchmark.Features.CustomData.Services;

public interface ICustomDataService
{
    Task UpsertCustomDataAsync(CustomDataSchool data);
    Task<CustomDataSchool?> CustomDataSchoolAsync(string urn, string identifier);
    Task InsertNewAndDeactivateExistingUserDataAsync(CustomDataUserData userData);
    Task<string> CurrentYearAsync();
    Task DeleteSchoolAsync(CustomDataSchool data);
}

[ExcludeFromCodeCoverage]
public class CustomDataService(IDatabaseFactory dbFactory) : ICustomDataService
{
    public async Task UpsertCustomDataAsync(CustomDataSchool data)
    {
        const string sql = "SELECT * from CustomDataSchool where URN = @URN AND Id = @Id ";

        var parameters = new
        {
            data.URN,
            data.Id
        };

        using var conn = await dbFactory.GetConnection();
        var existing = await conn.QueryFirstOrDefaultAsync<CustomDataSchool>(sql, parameters);

        using var transaction = conn.BeginTransaction();
        if (existing != null)
        {
            existing.Data = data.Data;
            await conn.UpdateAsync(existing, transaction);
        }
        else
        {
            await conn.InsertAsync(data, transaction);
        }

        transaction.Commit();
    }

    public async Task<CustomDataSchool?> CustomDataSchoolAsync(string urn, string identifier)
    {
        const string sql = "SELECT * from CustomDataSchool where URN = @URN AND Id = @Id";
        var parameters = new
        {
            URN = urn,
            Id = identifier
        };

        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<CustomDataSchool>(sql, parameters);
    }

    public async Task InsertNewAndDeactivateExistingUserDataAsync(CustomDataUserData userData)
    {
        const string sql = "UPDATE UserData SET Active = 0 where OrganisationId = @OrganisationId AND OrganisationType = @OrganisationType AND Type = @Type AND UserId = @UserId";

        var parameters = new
        {
            userData.OrganisationId,
            userData.OrganisationType,
            userData.Type,
            userData.UserId
        };

        using var conn = await dbFactory.GetConnection();
        using var transaction = conn.BeginTransaction();
        await conn.ExecuteAsync(sql, parameters, transaction);
        await conn.InsertAsync(userData, transaction);
        transaction.Commit();
    }

    public async Task<string> CurrentYearAsync()
    {
        const string sql = "SELECT Value from Parameters where Name = 'CurrentYear'";
        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstAsync<string>(sql);
    }

    public async Task DeleteSchoolAsync(CustomDataSchool data)
    {
        const string sql = "UPDATE UserData SET Status = @Removed, Active = 0 where Id = @Id";
        var parameters = new
        {
            data.Id,
            Pipeline.JobStatus.Removed
        };

        using var connection = await dbFactory.GetConnection();
        using var transaction = connection.BeginTransaction();

        await connection.DeleteAsync(data, transaction);
        await connection.ExecuteAsync(sql, parameters, transaction);

        transaction.Commit();
    }
}