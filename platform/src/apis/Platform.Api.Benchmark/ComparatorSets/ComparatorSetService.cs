using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper;
using Platform.Infrastructure.Sql;

namespace Platform.Api.Benchmark.ComparatorSets;

public interface IComparatorSetService
{
    Task<DefaultComparatorSet> DefaultAsync(string urn, string setType = "unmixed");
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
}