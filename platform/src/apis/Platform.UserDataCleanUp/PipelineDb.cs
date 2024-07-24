using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper;
using Platform.Infrastructure.Sql;
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Platform.UserDataCleanUp;

public record UserData
{
    public string? Id { get; set; }
    public string? Type { get; set; }
    public string? OrganisationType { get; set; }
}

public interface IPlatformDb
{
    Task<IEnumerable<UserData>> GetUserDataForDeletion();
    Task RemoveSchoolComparatorSet(string id);
    Task RemoveTrustComparatorSet(string id);
    Task RemoveCustomData(string id);
}

[ExcludeFromCodeCoverage]
public class PlatformDb(IDatabaseFactory dbFactory) : IPlatformDb
{

    public async Task<IEnumerable<UserData>> GetUserDataForDeletion()
    {
        const string sql = "SELECT * FROM UserData WHERE Status = 'removed' OR Expiry < GETUTCDATE()";
        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<UserData>(sql);
    }

    public async Task RemoveSchoolComparatorSet(string id)
    {
        const string comparatorSetSql = "DELETE FROM UserDefinedSchoolComparatorSet WHERE RunId = @Id";
        const string metricRAGSql = "DELETE FROM MetricRAG WHERE RunId = @Id AND RunType = 'default'";
        const string userDataSql = "DELETE FROM UserData WHERE Id = @Id AND Type = 'comparator-set' AND OrganisationType = 'school'";

        var parameters = new
        {
            Id = id
        };

        using var conn = await dbFactory.GetConnection();
        using var transaction = conn.BeginTransaction();

        await conn.ExecuteAsync(comparatorSetSql, parameters, transaction);
        await conn.ExecuteAsync(metricRAGSql, parameters, transaction);
        await conn.ExecuteAsync(userDataSql, parameters, transaction);

        transaction.Commit();

    }

    public async Task RemoveTrustComparatorSet(string id)
    {
        const string comparatorSetSql = "DELETE FROM UserDefinedTrustComparatorSet WHERE RunId = @Id";
        const string userDataSql = "DELETE FROM UserData WHERE Id = @Id AND Type = 'comparator-set' AND OrganisationType = 'trust'";

        var parameters = new
        {
            Id = id
        };

        using var conn = await dbFactory.GetConnection();
        using var transaction = conn.BeginTransaction();

        await conn.ExecuteAsync(comparatorSetSql, parameters, transaction);
        await conn.ExecuteAsync(userDataSql, parameters, transaction);

        transaction.Commit();

    }

    public async Task RemoveCustomData(string id)
    {
        const string comparatorSetSql = "DELETE FROM ComparatorSet WHERE RunId = @Id AND RunType = 'custom'";
        const string financialSql = "DELETE FROM Financial WHERE RunId = @Id AND RunType = 'custom'";
        const string metricRAGSql = "DELETE FROM MetricRAG WHERE RunId = @Id AND RunType = 'custom'";
        const string nonFinancialSql = "DELETE FROM NonFinancial WHERE RunId = @Id AND RunType = 'custom'";
        const string userDataSql = "DELETE FROM UserData WHERE Id = @Id AND Type = 'custom-data' AND OrganisationType = 'school'";

        var parameters = new
        {
            Id = id
        };

        using var conn = await dbFactory.GetConnection();
        using var transaction = conn.BeginTransaction();

        await conn.ExecuteAsync(comparatorSetSql, parameters, transaction);
        await conn.ExecuteAsync(financialSql, parameters, transaction);
        await conn.ExecuteAsync(metricRAGSql, parameters, transaction);
        await conn.ExecuteAsync(nonFinancialSql, parameters, transaction);
        await conn.ExecuteAsync(userDataSql, parameters, transaction);

        transaction.Commit();
    }
}