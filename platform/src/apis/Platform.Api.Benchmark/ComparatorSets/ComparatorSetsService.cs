using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Platform.Infrastructure.Sql;

namespace Platform.Api.Benchmark.ComparatorSets;

public interface IComparatorSetsService
{
    Task<string> CurrentYearAsync();
    Task<ComparatorSetDefault> DefaultAsync(string urn, string setType = "unmixed");
    Task UpsertUserDefinedSet(ComparatorSetUserDefined comparatorSet);
    Task<ComparatorSetUserDefined?> UserDefinedAsync(string urn, string identifier, string runtType = "default");
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

    public async Task<ComparatorSetDefault> DefaultAsync(string urn, string setType)
    {
        const string paramSql = "SELECT Value from Parameters where Name = 'CurrentYear'";
        const string setSql = "SELECT * from ComparatorSet where RunType = 'default' AND RunId = @RunId AND SetType = @SetType AND URN = @URN";

        using var conn = await _dbFactory.GetConnection();
        var year = await conn.QueryFirstAsync<string>(paramSql);
        var parameters = new { URN = urn, RunId = year, SetType = setType };
        return await conn.QueryFirstOrDefaultAsync<ComparatorSetDefault>(setSql, parameters);
    }

    public async Task UpsertUserDefinedSet(ComparatorSetUserDefined comparatorSet)
    {
        const string sql = "SELECT * from UserDefinedComparatorSet where URN = @URN AND RunId = @RunId AND RunType = @RunType";

        var parameters = new { comparatorSet.URN, comparatorSet.RunId, comparatorSet.RunType };

        using var conn = await _dbFactory.GetConnection();
        var existing = await conn.QueryFirstOrDefaultAsync<ComparatorSetUserDefined>(sql, parameters);

        using var transaction = conn.BeginTransaction();
        if (existing != null)
        {
            existing.Set = comparatorSet.Set;
            existing.Status = "pending";

            await conn.UpdateAsync(existing, transaction);
        }
        else
        {
            await conn.InsertAsync(comparatorSet, transaction);
        }

        transaction.Commit();
    }

    public async Task<ComparatorSetUserDefined?> UserDefinedAsync(string urn, string identifier, string runtType = "default")
    {
        const string sql = "SELECT * from UserDefinedComparatorSet where URN = @URN AND RunId = @RunId AND RunType = @RunType";
        var parameters = new { URN = urn, RunId = identifier, RunType = runtType };

        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<ComparatorSetUserDefined>(sql, parameters);
    }
}