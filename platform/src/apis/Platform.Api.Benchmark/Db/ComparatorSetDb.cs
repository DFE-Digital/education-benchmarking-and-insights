using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper;
using Platform.Domain.DataObjects;
using Platform.Domain.Responses;
using Platform.Infrastructure.Sql;

namespace Platform.Api.Benchmark.Db;

public interface IComparatorSetDb
{
    Task<ComparatorSet> Get(string urn);
}

[ExcludeFromCodeCoverage]
public class ComparatorSetDb : IComparatorSetDb
{
    private readonly IDatabaseFactory _dbFactory;

    public ComparatorSetDb(IDatabaseFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<ComparatorSet> Get(string urn)
    {
        const string sql = "SELECT * from ComparatorSets where URN = @URN";
        var parameters = new { URN = int.Parse(urn) };

        using var conn = await _dbFactory.GetConnection();
        var results = conn.Query<ComparatorDataObject>(sql, parameters);

        return ComparatorSet.Create(results);
    }
}