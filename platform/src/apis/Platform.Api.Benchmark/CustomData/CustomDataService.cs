using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Platform.Functions.Extensions;
using Platform.Infrastructure.Sql;

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
public class CustomDataService : ICustomDataService
{
    private readonly IDatabaseFactory _dbFactory;

    public CustomDataService(IDatabaseFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }


    public async Task UpsertCustomDataAsync(CustomDataSchool data)
    {
        const string sql = "SELECT * from CustomDataSchool where URN = @URN AND Id = @Id ";

        var parameters = new { data.URN, data.Id };

        using var conn = await _dbFactory.GetConnection();
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
        var parameters = new { URN = urn, Id = identifier };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<CustomDataSchool>(sql, parameters);
    }

    public async Task UpsertUserDataAsync(CustomDataUserData userData)
    {
        const string sql = "SELECT * from UserData where Id = @Id";

        var parameters = new { userData.Id };

        using var conn = await _dbFactory.GetConnection();
        var existing = await conn.QueryFirstOrDefaultAsync<CustomDataUserData>(sql, parameters);

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
        const string sql = "SELECT Value from Parameters where Name = 'CurrentYear'";
        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryFirstAsync<string>(sql);
    }

    public async Task DeleteSchoolAsync(CustomDataSchool data)
    {
        const string sql = "UPDATE UserData SET Status = 'removed' where Id = @Id";
        var parameters = new { data.Id };

        using var connection = await _dbFactory.GetConnection();
        using var transaction = connection.BeginTransaction();

        await connection.DeleteAsync(data, transaction);
        await connection.ExecuteAsync(sql, parameters, transaction);

        transaction.Commit();
    }
}