using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Platform.Infrastructure.Sql;

namespace Platform.Api.Benchmark.ComparatorSets;

public interface IComparatorSetsService
{
    Task<string> CurrentYearAsync();
    Task<ComparatorSetDefaultSchool> DefaultSchoolAsync(string urn, string setType = "unmixed");
    Task UpsertUserDefinedSchoolAsync(ComparatorSetUserDefinedSchool comparatorSet);
    Task<ComparatorSetUserDefinedSchool?> UserDefinedSchoolAsync(string urn, string identifier, string runType = "default");
    Task UpsertUserDataAsync(ComparatorSetUserData userData);
    Task DeleteSchoolAsync(ComparatorSetUserDefinedSchool comparatorSet);
    Task DeleteTrustAsync(ComparatorSetUserDefinedTrust comparatorSet);
    Task<ComparatorSetUserDefinedTrust?> UserDefinedTrustAsync(string companyNumber, string identifier, string runType = "default");
    Task UpsertUserDefinedTrustAsync(ComparatorSetUserDefinedTrust comparatorSet);
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

    public async Task<ComparatorSetDefaultSchool> DefaultSchoolAsync(string urn, string setType)
    {
        const string paramSql = "SELECT Value from Parameters where Name = 'CurrentYear'";
        const string setSql = "SELECT * from ComparatorSet where RunType = 'default' AND RunId = @RunId AND SetType = @SetType AND URN = @URN";

        using var conn = await _dbFactory.GetConnection();
        var year = await conn.QueryFirstAsync<string>(paramSql);
        var parameters = new { URN = urn, RunId = year, SetType = setType };
        return await conn.QueryFirstOrDefaultAsync<ComparatorSetDefaultSchool>(setSql, parameters);
    }

    public async Task UpsertUserDefinedSchoolAsync(ComparatorSetUserDefinedSchool comparatorSet)
    {
        const string sql = "SELECT * from UserDefinedSchoolComparatorSet where URN = @URN AND RunId = @RunId AND RunType = @RunType";

        var parameters = new { comparatorSet.URN, comparatorSet.RunId, comparatorSet.RunType };

        using var conn = await _dbFactory.GetConnection();
        var existing = await conn.QueryFirstOrDefaultAsync<ComparatorSetUserDefinedSchool>(sql, parameters);

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

    public async Task<ComparatorSetUserDefinedSchool?> UserDefinedSchoolAsync(string urn, string identifier, string runType = "default")
    {
        const string sql = "SELECT * from UserDefinedSchoolComparatorSet where URN = @URN AND RunId = @RunId AND RunType = @RunType";
        var parameters = new { URN = urn, RunId = identifier, RunType = runType };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<ComparatorSetUserDefinedSchool>(sql, parameters);
    }

    public async Task UpsertUserDataAsync(ComparatorSetUserData userData)
    {
        const string sql = "SELECT * from UserData where Id = @Id";

        var parameters = new { userData.Id };

        using var conn = await _dbFactory.GetConnection();
        var existing = await conn.QueryFirstOrDefaultAsync<ComparatorSetUserData>(sql, parameters);

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

    public async Task DeleteSchoolAsync(ComparatorSetUserDefinedSchool comparatorSet)
    {
        const string sql = "UPDATE UserData SET Status = 'removed' where Id = @Id";
        var parameters = new { Id = comparatorSet.RunId };

        using var connection = await _dbFactory.GetConnection();
        using var transaction = connection.BeginTransaction();

        await connection.DeleteAsync(comparatorSet, transaction);
        await connection.ExecuteAsync(sql, parameters, transaction);

        transaction.Commit();
    }

    public async Task DeleteTrustAsync(ComparatorSetUserDefinedTrust comparatorSet)
    {
        const string sql = "UPDATE UserData SET Status = 'removed' where Id = @Id";
        var parameters = new { Id = comparatorSet.RunId };

        using var connection = await _dbFactory.GetConnection();
        using var transaction = connection.BeginTransaction();

        await connection.DeleteAsync(comparatorSet, transaction);
        await connection.ExecuteAsync(sql, parameters, transaction);

        transaction.Commit();
    }

    public async Task<ComparatorSetUserDefinedTrust> UserDefinedTrustAsync(string companyNumber, string identifier, string runType = "default")
    {
        const string sql = "SELECT * from UserDefinedTrustComparatorSet where CompanyNumber = @CompanyNumber AND RunId = @RunId AND RunType = @RunType";
        var parameters = new { CompanyNumber = companyNumber, RunId = identifier, RunType = runType };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<ComparatorSetUserDefinedTrust>(sql, parameters);
    }

    public async Task UpsertUserDefinedTrustAsync(ComparatorSetUserDefinedTrust comparatorSet)
    {
        const string sql = "SELECT * from UserDefinedTrustComparatorSet where CompanyNumber = @CompanyNumber AND RunId = @RunId AND RunType = @RunType";

        var parameters = new { comparatorSet.CompanyNumber, comparatorSet.RunId, comparatorSet.RunType };

        using var conn = await _dbFactory.GetConnection();
        var existing = await conn.QueryFirstOrDefaultAsync<ComparatorSetUserDefinedTrust>(sql, parameters);

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
}