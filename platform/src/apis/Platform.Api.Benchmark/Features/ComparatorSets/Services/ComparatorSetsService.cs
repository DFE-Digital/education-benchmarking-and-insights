using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Platform.Api.Benchmark.Features.ComparatorSets.Models;
using Platform.Domain;
using Platform.Sql;

namespace Platform.Api.Benchmark.Features.ComparatorSets.Services;

public interface IComparatorSetsService
{
    Task<string> CurrentYearAsync(CancellationToken cancellationToken = default);
    Task<ComparatorSetSchool?> DefaultSchoolAsync(string urn, CancellationToken cancellationToken = default);
    Task<ComparatorSetSchool?> CustomSchoolAsync(string runId, string urn, CancellationToken cancellationToken = default);
    Task UpsertUserDefinedSchoolAsync(ComparatorSetUserDefinedSchool comparatorSet);
    Task<ComparatorSetUserDefinedSchool?> UserDefinedSchoolAsync(string urn, string identifier, string runType = Pipeline.RunType.Default, CancellationToken cancellationToken = default);
    Task InsertNewAndDeactivateExistingUserDataAsync(ComparatorSetUserData userData);
    Task DeleteSchoolAsync(ComparatorSetUserDefinedSchool comparatorSet);
    Task DeleteTrustAsync(ComparatorSetUserDefinedTrust comparatorSet);
    Task<ComparatorSetUserDefinedTrust?> UserDefinedTrustAsync(string companyNumber, string identifier, string runType = Pipeline.RunType.Default, CancellationToken cancellationToken = default);
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

    public async Task<string> CurrentYearAsync(CancellationToken cancellationToken = default)
    {
        const string sql = "SELECT Value from Parameters where Name = 'CurrentYear'";
        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryFirstAsync<string>(sql, cancellationToken: cancellationToken);
    }

    public async Task<ComparatorSetSchool?> DefaultSchoolAsync(string urn, CancellationToken cancellationToken = default)
    {
        const string paramSql = "SELECT Value from Parameters where Name = 'CurrentYear'";
        const string setSql = "SELECT * from ComparatorSet where RunType = 'default' AND RunId = @RunId AND URN = @URN";

        using var conn = await _dbFactory.GetConnection();
        var year = await conn.QueryFirstAsync<string>(paramSql, cancellationToken: cancellationToken);
        var parameters = new
        {
            URN = urn,
            RunId = year
        };
        return await conn.QueryFirstOrDefaultAsync<ComparatorSetSchool>(setSql, parameters, cancellationToken);
    }

    public async Task<ComparatorSetSchool?> CustomSchoolAsync(string runId, string urn, CancellationToken cancellationToken = default)
    {
        const string setSql = "SELECT * from ComparatorSet where RunType = 'custom' AND RunId = @RunId AND URN = @URN";

        using var conn = await _dbFactory.GetConnection();
        var parameters = new
        {
            URN = urn,
            RunId = runId
        };
        return await conn.QueryFirstOrDefaultAsync<ComparatorSetSchool>(setSql, parameters, cancellationToken);
    }

    public async Task UpsertUserDefinedSchoolAsync(ComparatorSetUserDefinedSchool comparatorSet)
    {
        const string sql = "SELECT * from UserDefinedSchoolComparatorSet where URN = @URN AND RunId = @RunId AND RunType = @RunType";

        var parameters = new
        {
            comparatorSet.URN,
            comparatorSet.RunId,
            comparatorSet.RunType
        };

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

    public async Task<ComparatorSetUserDefinedSchool?> UserDefinedSchoolAsync(string urn, string identifier, string runType = Pipeline.RunType.Default, CancellationToken cancellationToken = default)
    {
        const string sql = "SELECT * from UserDefinedSchoolComparatorSet where URN = @URN AND RunId = @RunId AND RunType = @RunType";
        var parameters = new
        {
            URN = urn,
            RunId = identifier,
            RunType = runType
        };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<ComparatorSetUserDefinedSchool>(sql, parameters, cancellationToken);
    }

    public async Task InsertNewAndDeactivateExistingUserDataAsync(ComparatorSetUserData userData)
    {
        const string sql = "UPDATE UserData SET Active = 0 where OrganisationId = @OrganisationId AND OrganisationType = @OrganisationType AND Type = @Type AND UserId = @UserId";

        var parameters = new
        {
            userData.OrganisationId,
            userData.OrganisationType,
            userData.Type,
            userData.UserId
        };

        using var conn = await _dbFactory.GetConnection();
        using var transaction = conn.BeginTransaction();
        await conn.ExecuteAsync(sql, parameters, transaction);
        await conn.InsertAsync(userData, transaction);
        transaction.Commit();
    }

    public async Task DeleteSchoolAsync(ComparatorSetUserDefinedSchool comparatorSet)
    {
        const string sql = "UPDATE UserData SET Status = @Removed, Active = 0 where Id = @Id";
        var parameters = new
        {
            Id = comparatorSet.RunId,
            Pipeline.JobStatus.Removed
        };

        using var connection = await _dbFactory.GetConnection();
        using var transaction = connection.BeginTransaction();

        await connection.DeleteAsync(comparatorSet, transaction);
        await connection.ExecuteAsync(sql, parameters, transaction);

        transaction.Commit();
    }

    public async Task DeleteTrustAsync(ComparatorSetUserDefinedTrust comparatorSet)
    {
        const string sql = "UPDATE UserData SET Status = @Removed, Active = 0 where Id = @Id";
        var parameters = new
        {
            Id = comparatorSet.RunId,
            Pipeline.JobStatus.Removed
        };

        using var connection = await _dbFactory.GetConnection();
        using var transaction = connection.BeginTransaction();

        await connection.DeleteAsync(comparatorSet, transaction);
        await connection.ExecuteAsync(sql, parameters, transaction);

        transaction.Commit();
    }

    public async Task<ComparatorSetUserDefinedTrust?> UserDefinedTrustAsync(string companyNumber, string identifier, string runType = Pipeline.RunType.Default, CancellationToken cancellationToken = default)
    {
        const string sql = "SELECT * from UserDefinedTrustComparatorSet where CompanyNumber = @CompanyNumber AND RunId = @RunId AND RunType = @RunType";
        var parameters = new
        {
            CompanyNumber = companyNumber,
            RunId = identifier,
            RunType = runType
        };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<ComparatorSetUserDefinedTrust>(sql, parameters, cancellationToken);
    }

    public async Task UpsertUserDefinedTrustAsync(ComparatorSetUserDefinedTrust comparatorSet)
    {
        const string sql = "SELECT * from UserDefinedTrustComparatorSet where CompanyNumber = @CompanyNumber AND RunId = @RunId AND RunType = @RunType";

        var parameters = new
        {
            comparatorSet.CompanyNumber,
            comparatorSet.RunId,
            comparatorSet.RunType
        };

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