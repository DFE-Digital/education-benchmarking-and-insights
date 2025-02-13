using System;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Platform.Domain;
using Platform.Orchestrator.Functions;
using Platform.Sql;

namespace Platform.Orchestrator.Sql;

public interface IPipelineDb
{
    Task<int> UpdateStatus(PipelineStatus status);
    Task<int> WriteToLog(string? orchestrationId, string? message);
}

public class PipelineDb(IDatabaseFactory dbFactory) : IPipelineDb
{
    public async Task<int> UpdateStatus(PipelineStatus status)
    {
        const string sql = "UPDATE UserData SET Status = @status where Id = @RunId";
        var parameters = new
        {
            status.RunId,
            status = status.Success ? Pipeline.JobStatus.Complete : Pipeline.JobStatus.Failed
        };

        using var conn = await dbFactory.GetConnection();
        using var transaction = conn.BeginTransaction();
        var rowsAffected = await conn.ExecuteAsync(sql, parameters, transaction);

        transaction.Commit();
        return rowsAffected;
    }

    public async Task<int> WriteToLog(string? orchestrationId, string? message)
    {
        using var connection = await dbFactory.GetConnection();
        using var transaction = connection.BeginTransaction();

        var newPlan = new CompletedPipelineRun
        {
            CompletedAt = DateTimeOffset.Now,
            OrchestrationId = orchestrationId,
            Message = message
        };

        var rowsAffected = await connection.InsertAsync(newPlan, transaction);

        transaction.Commit();
        return rowsAffected;
    }
}