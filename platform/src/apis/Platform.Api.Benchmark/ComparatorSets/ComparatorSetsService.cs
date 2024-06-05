using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Platform.Infrastructure.Sql;

namespace Platform.Api.Benchmark.ComparatorSets;

public interface IComparatorSetsService
{
    Task<string> CurrentYearAsync();
    Task<ComparatorSetDefault> DefaultSchoolAsync(string urn, string setType = "unmixed");
    Task UpsertUserDefinedSchoolAsync(ComparatorSetUserDefined comparatorSet);
    Task<ComparatorSetUserDefined?> UserDefinedSchoolAsync(string urn, string identifier, string runtType = "default");
    Task UpsertUserDataAsync(string identifier, string? userId, string status = "pending");
    Task DeleteSchoolAsync(ComparatorSetUserDefined comparatorSet);
}

[ExcludeFromCodeCoverage]
public class ComparatorSetsService : IComparatorSetsService
{
    private readonly IDatabaseFactory _dbFactory;

    public ComparatorSetsService(IDatabaseFactory dbFactory)
    {
        _dbFactory = dbFactory;
        SqlMapper.AddTypeHandler(new ComparatorSetTypeHandler());
    }

    public async Task<string> CurrentYearAsync()
    {
        const string sql = "SELECT Value from Parameters where Name = 'CurrentYear'";
        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryFirstAsync<string>(sql);
    }

    public async Task<ComparatorSetDefault> DefaultSchoolAsync(string urn, string setType)
    {
        const string paramSql = "SELECT Value from Parameters where Name = 'CurrentYear'";
        const string setSql = "SELECT * from ComparatorSet where RunType = 'default' AND RunId = @RunId AND SetType = @SetType AND URN = @URN";

        using var conn = await _dbFactory.GetConnection();
        var year = await conn.QueryFirstAsync<string>(paramSql);
        var parameters = new { URN = urn, RunId = year, SetType = setType };
        return await conn.QueryFirstOrDefaultAsync<ComparatorSetDefault>(setSql, parameters);
    }

    public async Task UpsertUserDefinedSchoolAsync(ComparatorSetUserDefined comparatorSet)
    {
        const string sql = "SELECT * from UserDefinedComparatorSet where URN = @URN AND RunId = @RunId AND RunType = @RunType";

        var parameters = new { comparatorSet.URN, comparatorSet.RunId, comparatorSet.RunType };

        using var conn = await _dbFactory.GetConnection();
        var existing = await conn.QueryFirstOrDefaultAsync<ComparatorSetUserDefined>(sql, parameters);

        using var transaction = conn.BeginTransaction();
        if (existing != null)
        {
            existing.Set = comparatorSet.Set;
            await conn.UpdateAsync(existing, transaction);
        }
        else
        {
            await conn.InsertAsync(comparatorSet, transaction);
        }

        transaction.Commit();
    }

    public async Task<ComparatorSetUserDefined?> UserDefinedSchoolAsync(string urn, string identifier, string runtType = "default")
    {
        const string sql = "SELECT * from UserDefinedComparatorSet where URN = @URN AND RunId = @RunId AND RunType = @RunType";
        var parameters = new { URN = urn, RunId = identifier, RunType = runtType };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<ComparatorSetUserDefined>(sql, parameters);
    }

    public async Task UpsertUserDataAsync(string identifier, string? userId, string status = "pending")
    {
        const string sql = "SELECT * from UserData where Id = @Id";

        var parameters = new { Id = identifier };

        using var conn = await _dbFactory.GetConnection();
        var existing = await conn.QueryFirstOrDefaultAsync<ComparatorSetUserData>(sql, parameters);

        using var transaction = conn.BeginTransaction();
        if (existing != null)
        {
            existing.Expiry = DateTimeOffset.Now.AddDays(30);
            existing.Status = status;
            await conn.UpdateAsync(existing, transaction);
        }
        else
        {
            var data = new ComparatorSetUserData
            {
                Id = identifier,
                UserId = userId,
                Expiry = DateTimeOffset.Now.AddDays(30),
                Status = status
            };

            await conn.InsertAsync(data, transaction);
        }

        transaction.Commit();
    }

    public async Task DeleteSchoolAsync(ComparatorSetUserDefined comparatorSet)
    {
        const string sql = "UPDATE UserData SET Status = 'removed' where Id = @Id";
        var parameters = new { Id = comparatorSet.RunId };

        using var connection = await _dbFactory.GetConnection();
        using var transaction = connection.BeginTransaction();

        await connection.DeleteAsync(comparatorSet, transaction);
        await connection.ExecuteAsync(sql, parameters, transaction);

        transaction.Commit();
    }
}