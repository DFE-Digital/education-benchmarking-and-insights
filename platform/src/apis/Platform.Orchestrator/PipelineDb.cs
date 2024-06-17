using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Platform.Infrastructure.Sql;

namespace Platform.Orchestrator;

public interface IPipelineDb
{
    Task UpdateStatus(string? id);
    void WriteToLog(string? orchestrationId, string? message);
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

    public async void WriteToLog(string? orchestrationId, string? message)
    {
        using var connection = await _dbFactory.GetConnection();
        using var transaction = connection.BeginTransaction();

        var newPlan = new CompletedPipelineRun
        {
            CompletedAt = DateTimeOffset.Now,
            OrchestrationId = orchestrationId,
            Message = message
        };

        await connection.InsertAsync(newPlan, transaction);

        transaction.Commit();
    }
}

[ExcludeFromCodeCoverage]
[Table("CompletedPipelineRun")]
public record CompletedPipelineRun
{
    [Key] public int Id { get; set; }
    public DateTimeOffset CompletedAt { get; set; }
    public string? OrchestrationId { get; set; }
    public string? Message { get; set; }
}