using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper;
using Platform.Infrastructure.Sql;

namespace Platform.Orchestrator;

public interface IPipelineDb
{
    Task UpdateComparatorSetStatus(string? urn, string? runId, string? runType);
}


[ExcludeFromCodeCoverage]
public class PipelineDb : IPipelineDb
{
    private readonly IDatabaseFactory _dbFactory;

    public PipelineDb(IDatabaseFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task UpdateComparatorSetStatus(string? urn, string? runId, string? runType)
    {
        const string sql = "UPDATE UserDefinedComparatorSet SET Status = 'complete' where URN = @URN AND RunId = @RunId AND RunType = @RunType";
        var parameters = new { URN = urn, RunId = runId, RunType = runType };

        using var conn = await _dbFactory.GetConnection();
        using var transaction = conn.BeginTransaction();
        await conn.ExecuteAsync(sql, parameters, transaction);

        transaction.Commit();
    }
}