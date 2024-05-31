using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Platform.Infrastructure.Sql;

namespace Platform.Api.Benchmark.ComparatorSets;

public interface IComparatorSetService
{
    Task<DefaultComparatorSet> DefaultAsync(string urn, string setType = "unmixed");
    Task UpsertUserDefinedSet(UserDefinedComparatorSet comparatorSet);
    Task<UserDefinedComparatorSet?> UserDefinedAsync(string urn, string identifier, string runtType = "default");
}

[ExcludeFromCodeCoverage]
public class ComparatorSetService : IComparatorSetService
{
    private readonly IDatabaseFactory _dbFactory;

    public ComparatorSetService(IDatabaseFactory dbFactory)
    {
        _dbFactory = dbFactory;
        SqlMapper.AddTypeHandler(new ComparatorSetTypeHandler());
    }

    public async Task<DefaultComparatorSet> DefaultAsync(string urn, string setType)
    {
        const string paramSql = "SELECT Value from Parameters where Name = 'CurrentYear'";
        const string setSql = "SELECT * from ComparatorSet where RunType = 'default' AND RunId = @RunId AND SetType = @SetType AND URN = @URN";

        using var conn = await _dbFactory.GetConnection();
        var year = await conn.QueryFirstAsync<string>(paramSql);
        var parameters = new { URN = urn, RunId = year, SetType = setType };
        return await conn.QueryFirstOrDefaultAsync<DefaultComparatorSet>(setSql, parameters);
    }

    public async Task UpsertUserDefinedSet(UserDefinedComparatorSet comparatorSet)
    {
        const string sql = "SELECT * from UserDefinedComparatorSet where URN = @URN AND RunId = @RunId AND RunType = @RunType";
        
        var parameters = new {comparatorSet.URN, comparatorSet.RunId, comparatorSet.RunType };
        
        using var conn = await _dbFactory.GetConnection();
        var existing = await conn.QueryFirstOrDefaultAsync<UserDefinedComparatorSet>(sql, parameters);
        
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

    public async Task<UserDefinedComparatorSet?> UserDefinedAsync(string urn, string identifier, string runtType = "default")
    {
        const string sql = "SELECT * from UserDefinedComparatorSet where URN = @URN AND RunId = @RunId AND RunType = @RunType";
        var parameters = new {URN = urn, RunId = identifier, RunType = runtType };
        
        using var conn = await _dbFactory.GetConnection();
        return await conn.QueryFirstOrDefaultAsync<UserDefinedComparatorSet>(sql, parameters);
    }
}