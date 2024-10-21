using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Platform.Sql;
namespace Platform.Api.Benchmark.ComparatorSets;

public interface IComparatorSetsService
{
    Task<string> CurrentYearAsync();
    Task<ComparatorSetSchool?> DefaultSchoolAsync(string urn);
    Task<ComparatorSetSchool?> CustomSchoolAsync(string runId, string urn);
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
        SqlMapper.AddTypeHandler(new ComparatorSetIdsTypeHandler());
    }

    public async Task<string> CurrentYearAsync()
    {
        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryFirstAsync<string>(Queries.GetCurrentYear);
    }

    public async Task<ComparatorSetSchool?> DefaultSchoolAsync(string urn)
    {
        using var conn = await _dbFactory.GetConnection();
        var year = await conn.QueryFirstAsync<string>(Queries.GetCurrentYear);
        
        var template = Queries.GetDefaultComparatorSet(urn, year);
        return await conn.QueryFirstOrDefaultAsync<ComparatorSetSchool>(template);
    }

    public async Task<ComparatorSetSchool?> CustomSchoolAsync(string runId, string urn)
    {
        using var conn = await _dbFactory.GetConnection();
        var template = Queries.GetCustomComparatorSet(urn, runId);
        return await conn.QueryFirstOrDefaultAsync<ComparatorSetSchool>(template);
    }

    public async Task UpsertUserDefinedSchoolAsync(ComparatorSetUserDefinedSchool comparatorSet)
    {
        var template = Queries.GetUserDefinedSchoolComparatorSet(comparatorSet.URN, comparatorSet.RunId, comparatorSet.RunType);
        
        using var conn = await _dbFactory.GetConnection();
        var existing = await conn.QueryFirstOrDefaultAsync<ComparatorSetUserDefinedSchool>(template);

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
        var template = Queries.GetUserDefinedSchoolComparatorSet(urn, identifier, runType);
        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<ComparatorSetUserDefinedSchool>(template);
    }

    public async Task UpsertUserDataAsync(ComparatorSetUserData userData)
    {
        using var conn = await _dbFactory.GetConnection();
        
        var template = Queries.GetUserDataById(userData.Id);
        var existing = await conn.QueryFirstOrDefaultAsync<ComparatorSetUserData>(template);

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
        var template = Queries.UpdateUserDataSetStatusRemoved(comparatorSet.RunId);
        
        using var connection = await _dbFactory.GetConnection();
        using var transaction = connection.BeginTransaction();

        await connection.DeleteAsync(comparatorSet, transaction);
        await connection.ExecuteAsync(template, transaction);

        transaction.Commit();
    }

    public async Task DeleteTrustAsync(ComparatorSetUserDefinedTrust comparatorSet)
    {
        var template = Queries.UpdateUserDataSetStatusRemoved(comparatorSet.RunId);
        
        using var connection = await _dbFactory.GetConnection();
        using var transaction = connection.BeginTransaction();

        await connection.DeleteAsync(comparatorSet, transaction);
        await connection.ExecuteAsync(template, transaction);

        transaction.Commit();
    }

    public async Task<ComparatorSetUserDefinedTrust?> UserDefinedTrustAsync(string companyNumber, string identifier, string runType = "default")
    {
        var template = Queries.GetUserDefinedTrustComparatorSet(companyNumber, identifier, runType);
        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<ComparatorSetUserDefinedTrust>(template);
    }

    public async Task UpsertUserDefinedTrustAsync(ComparatorSetUserDefinedTrust comparatorSet)
    {
        var template = Queries.GetUserDefinedTrustComparatorSet(comparatorSet.CompanyNumber, comparatorSet.RunId, comparatorSet.RunType);
        
        using var conn = await _dbFactory.GetConnection();
        var existing = await conn.QueryFirstOrDefaultAsync<ComparatorSetUserDefinedTrust>(template);

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