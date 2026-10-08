using System;
using System.Threading.Tasks;
using Platform.Domain;
using Platform.Orchestrator.Functions;
using Platform.Sql;

namespace Platform.Orchestrator.Sql;

public interface IPipelineDb
{
    Task<int> UpdateUserDataStatus(PipelineStatus status);
    Task<int> WriteToLog(string? orchestrationId, string? message);
    Task<int> DeactivateUserData();
    Task<int> WriteLocalAuthorityRisksFileDetails(FileRecord file);
}

public class PipelineDb(IDatabaseFactory dbFactory) : IPipelineDb
{
    public async Task<int> UpdateUserDataStatus(PipelineStatus status)
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

    public async Task<int> DeactivateUserData()
    {
        const string sql = "UPDATE UserData SET Active = 0";

        using var conn = await dbFactory.GetConnection();
        using var transaction = conn.BeginTransaction();
        var rowsAffected = await conn.ExecuteAsync(sql, transaction: transaction);

        transaction.Commit();
        return rowsAffected;
    }

    public async Task<int> WriteLocalAuthorityRisksFileDetails(FileRecord file)
    {
        using var connection = await dbFactory.GetConnection();
        using var transaction = connection.BeginTransaction();

        // TODO: (UPDLOCK, HOLDLOCK)? race conditions are possible?
        // unlikely in our use case, but we should consider this
        const string sql = """

                                   IF EXISTS (
                                       SELECT 1
                                       FROM dbo.[File]
                                       WHERE Type = @Type
                                         AND Label = @Label
                                         AND RunId = @RunId
                                   )
                                   BEGIN
                                       UPDATE dbo.[File]
                                       SET FileName = @FileName,
                                           ValidFrom = @ValidFrom,
                                           Filesize = @Filesize
                                       WHERE Type = @Type
                                         AND Label = @Label
                                         AND RunId = @RunId;
                                   END
                                   ELSE
                                   BEGIN
                                       INSERT INTO dbo.[File] (Type, Label, FileName, ValidFrom, RunId, Filesize)
                                       VALUES (@Type, @Label, @FileName, @ValidFrom, @RunId, @Filesize);
                                   END
                           """;

        var parameters = new
        {
            file.Type,
            file.Label,
            file.FileName,
            file.ValidFrom,
            file.ValidTo,
            file.RunId,
            file.Filesize
        };

        var rowsAffected = await connection.ExecuteAsync(sql, parameters, transaction);

        transaction.Commit();
        return rowsAffected;
    }
}
