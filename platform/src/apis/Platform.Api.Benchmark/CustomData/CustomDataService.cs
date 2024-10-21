using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Platform.Sql;

namespace Platform.Api.Benchmark.CustomData;

public interface ICustomDataService
{
    Task UpsertCustomDataAsync(CustomDataSchool data);
    Task<CustomDataSchool?> CustomDataSchoolAsync(string urn, string identifier);
    Task UpsertUserDataAsync(CustomDataUserData userData);
    Task<string> CurrentYearAsync();
    Task DeleteSchoolAsync(CustomDataSchool data);
}

[ExcludeFromCodeCoverage]
public class CustomDataService(IDatabaseFactory dbFactory) : ICustomDataService
{
    public async Task UpsertCustomDataAsync(CustomDataSchool data)
    {
        var template = Queries.GetCustomData(data.URN, data.Id); 

        using var conn = await dbFactory.GetConnection();
        var existing = await conn.QueryFirstOrDefaultAsync<CustomDataSchool>(template);

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
        var template = Queries.GetCustomData(urn, identifier); 
        
        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<CustomDataSchool>(template);
    }

    public async Task UpsertUserDataAsync(CustomDataUserData userData)
    {
        var template = Queries.GetUserDataById(userData.Id);
        
        using var conn = await dbFactory.GetConnection();
        var existing = await conn.QueryFirstOrDefaultAsync<CustomDataUserData>(template);

        using var transaction = conn.BeginTransaction();
        if (existing != null)
        {
            existing.Expiry = userData.Expiry;
            existing.Status = userData.Status;
            await conn.UpdateAsync(existing, transaction);
        }
        else
        {
            await conn.InsertAsync(userData, transaction);
        }

        transaction.Commit();
    }

    public async Task<string> CurrentYearAsync()
    {
        using var conn = await dbFactory.GetConnection();
        return await conn.QueryFirstAsync<string>(Queries.GetCurrentYear);
    }

    public async Task DeleteSchoolAsync(CustomDataSchool data)
    {
        var template = Queries.UpdateUserDataSetStatusRemoved(data.Id);
        
        using var connection = await dbFactory.GetConnection();
        using var transaction = connection.BeginTransaction();

        await connection.DeleteAsync(data, transaction);
        await connection.ExecuteAsync(template, transaction);

        transaction.Commit();
    }
}