using System.Diagnostics.CodeAnalysis;
using Dapper;
using Platform.Domain;
using Platform.Infrastructure.Sql;

namespace Platform.Api.Insight.Features.ComparatorSets;

public interface IComparatorSetsDb
{
    Task<ComparatorSetResponseModel?> Get(int id);
}

[ExcludeFromCodeCoverage]
public class ComparatorSetsDb(IDatabaseFactory dbFactory) : IComparatorSetsDb
{
    public async Task<ComparatorSetResponseModel?> Get(int id)
    {
        const string sql = "SELECT * from ComparatorSets where URN = @URN";
        var parameters = new { URN = id };

        using var conn = await dbFactory.GetConnection();
        var results = conn.Query<ComparatorDataObject>(sql, parameters).ToArray();

        return results.Length != 0 ? ComparatorSetResponseModel.Create(results) : null;
    }
}