using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Platform.Domain.DataObjects;
using Platform.Domain.Responses;
using Platform.Infrastructure.Sql;

namespace Platform.Api.Benchmark.Db;

public interface IComparatorSetDb
{
    Task<ComparatorSet> Get(string urn, string peerGroup, string costGroup);
}

[ExcludeFromCodeCoverage]
public class ComparatorSetDb : IComparatorSetDb
{
    private readonly IDatabaseFactory _dbFactory;

    public ComparatorSetDb(IDatabaseFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<ComparatorSet> Get(string urn, string peerGroup, string costGroup)
    {
        var parameters = new { URN = int.Parse(urn), PeerGroup = peerGroup, CostGroup = costGroup };
        const string sql = "SELECT * from ComparatorSets where URN = @URN AND PeerGroup = @PeerGroup AND CostGroup = @CostGroup";

        using var conn = await _dbFactory.GetConnection();
        var result = conn.Query<ComparatorDataObject>(sql, parameters);

        return ComparatorSet.Create(result.Select(x => x.UKPRN_URN2.Split("_")[1]));
    }
}