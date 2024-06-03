using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper;
using Platform.Infrastructure.Sql;

namespace Platform.Orchestrator;

public interface IPipelineDb
{
    Task UpdateStatus(string? id);
}


[ExcludeFromCodeCoverage]
public class PipelineDb : IPipelineDb
{
    private readonly IDatabaseFactory _dbFactory;

    public PipelineDb(IDatabaseFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task UpdateStatus(string? id)
    {
        const string sql = "UPDATE UserData SET Status = 'complete' where Id = @Id";
        var parameters = new { Id = id };

        using var conn = await _dbFactory.GetConnection();
        using var transaction = conn.BeginTransaction();
        await conn.ExecuteAsync(sql, parameters, transaction);

        transaction.Commit();
    }
}